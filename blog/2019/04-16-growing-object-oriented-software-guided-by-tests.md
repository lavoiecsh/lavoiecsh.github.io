---
title: "Book Review: Growing Object-Oriented Software, Guided by Tests"
tags: [Review]
---
Today I'll be doing a review of the book "Growing Object-Oriented Software, Guided by Tests" by Steve Freeman and Nat Pryce.

Most people are aware of the TDD cycle of Fail - Pass - Refactor, this book uses this concept at a bigger level by doing acceptance tests with this cycle, which drives smaller cycles for integration and unit tests. It has a very good example solution which helps to understand how everything fits together on a larger scale.

<!-- truncate -->

### Who is this book for?
People who already have a basic understanding of Test Driven Development might find a new possibility in writing acceptance tests that drive all the tests and code. The book also briefly explains the basic concepts of Test Driven Development, so people who aren't used to TDD can pick it up too.

### What should be read in this book?
I think that this book should be read from cover to cover. The basic concepts at the start of the book are very theoretical but well explained and the example given after helps cement your understand.

### What should be retained from this book?
All the basic concepts of Acceptance Test Driven Development and the larger test cycle. There is whoever a small section at the beginning of the example which I would not recommend retaining: the walking skeleton.

The walking skeleton is a technique he uses to start a new project and consists of writing a test that will force you to implement a basic continuous integration and testing platform. However these days with the advent of platforms that handle a big part of it for you like Bitbucket's pipelines or Microsoft's Azure DevOps (formerly VSTS), this practice isn't as necessary as it may once have been (the book was written in 2009, the tools came out around 2017). The reason being that these tools allow you to quickly start a new project with continuous integration already built in with a simple button click.

I do however encourage starting your project with a simple test that just starts up the application and ensures that your testing libraries can interract with it.

### How does this apply to proper code?
Test Driven Development is a core concept of proper coding techniques that helps you ensure that the functionality is working, decouple your objects and write maintainable code. Doing Acceptance Test Driven Development makes it easier to integrate automated acceptance tests in your development cycle as you write these first. It also ensures that you don't forget a part of the code needed to implement the new feature. When using Git Flow for managing branches, it's especially powerful as the first thing you do in your new branch will be an acceptance test, and your branch is ready to merge when that acceptance test passes. It should also be noted that this pattern can also be used when fixing bugs by writing a test that follows the expected behaviour of the program (which should fail because there's a bug) and adding tests and code until that test passes (essentially fixing the bug).
