---
layout: post
title: "\"Service\" should be a banned word"
lang: en
ref: service
date: 2019-06-04
categories: [Practices]
tags: []
---
Just like "Manager" or "Data", I think "Service" is another word we should ban from our code. When you read Domain Driven Design theory, services are there to help you correctly separate concerns and contain some domain logic while handling other domain objects. Although services are an integral part of DDD, it's not a design pattern so you shouldn't have a class name containing "Service".

### It leads to unmaintainable code
We have an expression in french to describe something that contains everything: "fourre-tout", literally fill-everything. This describes exactly what happens when you have a class whose name contains "Service".

Let's say you're working on a marketplace application, at some point you'll probably have a cart to which you can add items. Your first reflex is probably to create a CartService which will handle everything pertaining to the cart. You can create a new cart, add an item to your cart, remove an item from you cart, change the quantity of an item in your cart, process that cart, delete that cart, etc. Already we're seeing six methods. If we average each method to 10 lines or so (including private functions needed for each main function), that's already 60 lines. Add the imports and other obligatory information, you're probably close to 100 lines. Now go back to those six methods: each method now counts as 10 percent of the file overall. If you're looking for an information in that class, 90 percent of the file is probably useless to you. Every time you add a new feature, you'll have to ask yourself "where do a put this new method?".

Now look at the tests for this class. Six methods each with around five unit tests: that's 30 unit tests, probably all in the same file name "CartServiceTest". That's beginning to be a lot harder to manage.

If you're also using the repository pattern incorrectly (see my [previous post]({% post_url 2019-05-22-crudrepository-en %})), you're probably also adding methods in this service to translate between your domain cart and your database cart and call the appropriate method of your repository. All these methods will probably have a test or two also. This CartService is rapidly getting to an unmaintainable size. Same goes for the CartServiceTest.

Another problem arrising from large files containing everything is merging problems when working with multiple people. Two stories can easily force you to work on the same huge class although they're in different features. If two people take one story each, there's bound to be some merging to do at some point.

### How do you fix this?

#### Make use of the repository pattern correctly
It should receive and return domain objects. This allows it to be used directly within your domain, reducing the need for translation methods in your services. If you don't have translation methods in your domain, you also don't have tests for these translation methods in your domain, reducing "gravy" methods and concentrating the domain on the important stuff.

#### Think Single Responsibility Principle
Each class should have one responsibility and it should do it well. Separate all those methods into different classes and name these classes according to what they do. Remember that every class should be a noun and every method should be a verb. Going back to the cart example, you could have CartCreator.Create, CartItemAdder.Add, CartItemRemover.Remove, etc. Also separate your test file into different tests for each feature.

### How does this fix things?
Each new class has only one responsibility, it's not crowded with methods not pertaining to that responsibility, and the tests associated are also separated so it's easy to see all the business rules associated with a feature: they're all in a single code file and a single test file. If you find that your class is becomming too large because it's particular feature has a lot of business rules, nothing stops you from extracting these into a separate class, or grouping common rules between multiple features into a shared class. At least each fonctionality has a single starting point, so it's still easy to navigate to it.

As you start adding more and more features in the long term, it's also easier to know where to add a new feature (just create a new file) or where to find an existing feature (having a good folder structure helps, but most editors can easily find a file within your whole project in an instant).

As a bonus, you're also removing the merging problem arrising from two people working on different features at the same time.
