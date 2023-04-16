---
title: "Thinking in resolvers"
tags: [Technologies,GraphQL]
---
# Thinking in Resolvers

Everything in GraphQL revolves around resolvers. It's important to understand this to better develop application exposing GraphQL APIs.

<!-- truncate -->

## Table of Contents
1. [Resolver Anatomy](#resolver-anatomy)
1. [Field within a type](#field-within-a-type)
1. [The special (but not so special) cases](#the-special-but-not-so-special-cases)
1. [Federation](#federation)
1. [Directives](#directives)

## Resolver Anatomy
A resolver is a function returning the value of a field within a type. It takes in four arguments:
1. parent: the instance returned by the parent resolver
1. args/input: any additional input to the field (usually what's in parentheses in the schema)
1. context: an object built by the server before resolver each query that usually contains globally accessible information and information about the current query
1. info: information about the fields, types, queries being executed

Most of the time we use the first three arguments to resolve a field. The fourth argument is usually discarded as the information is seldom useful.

For most GraphQL server implementations, resolvers are referenced within a hierarchy of Type to field.

## Field within a type
Each field within a type uses a resolver to define the value to return for this field and a particular instance of the parent type. Looking at the following schema:
```graphql
type Product {
  id: ID!
  type: ProductType!
  name(locale: Locale): String
}
```

This product type has three fields: id, type and name. Assuming the user wants all three fields to be returned and the product instance is already resolved, three resolvers will be called to get the value for aech field. In each case the parent parameter will be the instance of the product type that has been resolved previously and the context will receive what was built by the server. Additionally, the args parameter will be empty for the id and type fields, and contain the given locale for the name field. Each of these resolvers will be included in the Product type.

There is a default resolver that takes the value from the parent if that parent has a property matching the field we are resolving. You can override this resolver to add more logic when resolving the field:
- Not returning the value if the user doesn't have sufficient permissions
- Returning a different value in some cases
- Returning a calculated value
- Fetching data from a different table/collection in your database

## The special (but not so special) cases
Query, Mutation and Subscription types in GraphQL let you define the basic API you are exposing to the clients. These are referred to as the "root" types and are predefined by the specification.

These are still types in the sense that they contain fields that will be resolved through resolvers just like any other field within a type. The only different is that there is no default resolver for these types and you must then write the resolvers yourself. As for the arguments, they function the same way as for other field resolvers, although the parent parameter will be defined by the GraphQL implementation you use (usually null or an empty object).

Since these are still types from the GraphQL point of view, each query is a field with a resolver within the Query type, each mutation is a field with a resolver within the Mutation type, and each subscription is a field with a resolver within the Subscription type.

## Federation
Federation lets you define fields on types outside your domain and lets you return references to types outside your domain for fields in your domain. The gateway will handle which resolver to call for each field.

### Allowing extensions
When creating a type that could be extended by other services, you need to use the `@key` directive to indicate which fields will be exposed to the other services.

```graphql
type Product @key(fields: "id state") {
  id: ID!
  state: ProductState!
  type: ProductType!
  ...
}
```
The key in this case will contain the id and state fields, but not the type field.

### Extending
When extending a type to add a new field, you create an extended type in your schema using the `@extends` keyword. The `@key` and `@external` directives indicates the fields that are required for you to be able to resolve your additional field, and that these are coming from another service.

```graphql
@extends type Product @key(fields: "id state") {
  id: ID! @external
  state: ProductState! @external
  integrationConfiguration: IntegrationConfiguration
}
```

When resolving the integrationConfiguration field for a product, the gateway will send the id and state in the parent parameter of the resolver, allowing you to correctly resolve the integrationConfiguration field. This resolver will belong to the Product type within the integration configuration service.

### Referencing
When referencing an outside type within your schema, you need to include an __typename and all the key fields so that the gateway can resolve this reference. For example, again from the integration configuration service:
```graphql
type IntegrationConfiguration {
  id: ID!
  state: ProductState!
  products: [Product]
}
```

To create a backwards reference to the products linked to this integration configuration, you need to create a resolver named `products` in the IntegrationConfiguration type. This resolver will need to return an array of objects containing the id and state of the referenced product as well as the type.
```json
{
  "__typename": "Product",
  "id": "123",
  "state": "WORKING"
}
```

### Resolving References
With the resolved value above, the gateway then turns to the product service and asks to resolve the reference through the `__resolveReference` resolver. This is a special case resolver which only takes the parent and context arguments. The parent will contain the value for the key fields returned by the other service and the context will be generated just like any other resolver. This special resolver will be part of the `Product` type within the product service.

### Notes on Queries
With federation, queries will be coming to your service from the GraphQL gateway. These are usually a little obscure but it's useful to know what they look like so you can use them when testing resolvers without the gateway.
```graphql
query {
  _entities(representations: [
    {
      __typename: "Product"
      id: "123"
      state: WORKING
    }
  ]) {
    ... on Product {
      type
    }
  }
}
```

This kind of query is used by the gateway to resolve references to objects and additional fields defined in another service with federation. The representations in the `_entities` query defines all the parent objects with all the required fields to resolve whatever was requested by the client.

Since the type field resides in the Product type definition for the product service, it will first resolve the product through the `__resolveReference` resolver, passing in the given representation. It will then resolve the type field on the result. This can be used to ensure our `__resolveReference` resolver works as expected.

If we ask for the integrationConfiguration field instead of the type field in the same query, we get a query being sent to the integration configuration service to resolve the integration configuration. This will call the integrationConfiguration resolver within the Product type in the intgeration configuration service and pass as parent the given representation. This can be used to test additional fields for extended types in isolation, without needing to run the gateway and the service where the type was originally defined.

Representations must contain all the fields defined in the key directive for a type as well as the `__typename`. Notice how this is identical to what we return when we want to resolve a type belonging to another service.

## Directives
Once resolvers are clear, you can start to look at directives. A directive is an additional element to the schema to add logic around a resolver. Some directives are predefined by the specification. Federation adds some we previously mentioned:
- The `@extends` directive on a type indicates the type we are referencing is part of another service within the global schema. This ensures there are no collisions within a federated schema.
- The `@key` directive on a type ensures the key fields are resolved for an instance and extracts them for referencing within federation.
- The `@external` directive on a field indicates the field is not resolved by our service.

Directives are executed not when the field is resolved but when the server is started. This means you do not have access to the parent, input and context at the moment the field is resolved, but you can exchange the defined resolver with another one. For those fluent in Object-Oriented design patterns, you can think of them as decorators.

Directives can act on a multitude of elements within a GraphQL schema, from the schema itself, to types, fields, enum, enum values, scalars, arguments, input fields, etc. Typical use cases for using directives on fields is to perform validations on the input or verifying the user has the correct authorization before calling the resolver. They can also be used to modify the arguments being sent, such as lower-casing strings, or modify the return value from the resolver, suach as formatting or translating the output.

When adding validations to a query or mutation, remember these are still fields within a Query or Mutation type. You need to define a field directive on the query o rmutation. Within the directive implementation, you will then have access to the resolver function being called and can wrap it to validate the input being sent to the query or mutation.
