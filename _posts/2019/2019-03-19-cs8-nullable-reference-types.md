---
layout: post
title: "New stuff in C#8: Nullable Reference Types"
ref: cs8_nullable_reference_types
date: 2019-03-19
categories: [Technologies]
tags: [C#]
---
Today I'm starting a small series on new features coming to C#8.0, starting with Nullable Reference Types.

C# has a type system, like many other languages, which allows you to assign null to any non-primitive object (pretty much everything except integers, floating point numbers and characters). This means that whenever you receive a reference type through a function parameter or keep one as an instance variable, you need to make sure that it's not null before using it (or deal with the NullReferenceException). This also meant that you couldn't have nullable reference types (ex: `string?` or `Person?`).

One of the new features coming is a compilation flag you can add to your project to make reference types require non-null values, while also allowing to create nullable reference types. This explicits the fact that your variable or parameter may be null and it's ok because you handle it.

The only downside right now in the preview is that it is not enforced at compilation time, it only shows a warning when you're not initializing a non-nullable reference type or when you use it:

```C#
class Person {
    private string firstName; // non-nullable string
    public string LastName { get; set; }
    
    public Person() {
        // this constructor has the following warnings:
        // Non-nullable field 'firstName' is uninitialized.
        // Non-nullable property 'LastName' is uninitialized.
        
        // this should idealy not compile or initialize the field and property with default values
    }
    
    public Person(Person copy) {
        firstName = copy.firstName;
        LastName = copy.LastName;
    }
    
    public string FirstName() => firstName;
}
```

```C#
[TestClass]
class PersonTest {
    [TestMethod]
    public void CreatesPersonFromNothing() {
        var person = new Person();
        // both of these assertions will pass (although the field and property shouldn't be null) 
        // and show the following warnings:
        // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
        Assert.AreEqual(null, person.FirstName());
        Assert.AreEqual(null, person.LastName);
        // this should fail because the field and property can't be null
    }
    
    [TestMethod]
    public void ThrowsExceptionWhenCreatingPersonFromNull() {
        // this will show the warning: 
        // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
        Assert.ThrowsException<NullReferenceException>(() => new Person(null));
        // this shouldn't compile because you're passing null to a non-nullable reference type
    }
}
```

So all in all, I like this addition but would prefer having compilation errors and default initializations (empty string or parameter-less constructor). This would make sure you always have a value, either by initializing it for you in the case of fields and properties or by forcing you to pass a non-null value as a function parameter. Having the compilation flag means that we won't be coding defensively (making sure objects aren't null before using them), so we need a way to ensure our object isn't null at compilation time.