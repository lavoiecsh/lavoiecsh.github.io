---
layout: post
title: "Indentation"
lang: en
ref: indentation
date: 2019-08-06
categories: [Practices]
tags: []
---
Over the years, many styles of indentation have surfaced. From K&R to Allman, Whitesmiths or GNU, using tabs vs spaces, there are a lot of choices. So how do you pick one?

I will not advocate for a single style, but will give you guidelines on how to choose one that will last long and reduce the headaches that could be created with refactorings and such.

### Why tabs or spaces?
This has been a long debate for which both sides have emitted a number of arguments. Arguments for using tabs often include:
- reduced disk space: since you're using 1 tab instead of multiple spaces, in the long run it can save disk space. This was a big problem in the early days of programming, but with the amount of disk space and memory we have today, it's pretty much been voided since then.
- allows user preference: if you prefer 4 spaces per tab and your colleague prefers 8 spaces per tab, you can each define the tab size and it should not interfere with each other. Take care to use a continuation style that allows for this (more on this further in the article).
On the other side, for spaces the usual arguments are: 
- universal standard: everybody sees the same code the same way.
- it allows for partial tabs: some like this, but as I will demonstrate shortly, this is an argument I don't agree with.

### Continuation lines
One of the main problems with indentation is continuation lines. Two main problems can arise if you align your arguments together. 
Firstly, if you use tabs to indent (and probably spaces to fix the last part of the indentation, often refered to as smart tabs), different tab sizes will misalign the code:
```java
// with tab-size = 4, 3 tabs + 3 characters
public int add(int a,
               int b) {
    return a + b;
}
// with tab-size = 8, 3 tabs + 3 characters
public int add(int a,
                           int b) {
        return a + b;
}
```
The first version may look good, but the second version is already harder to read, even with such a simple example.

The second problem that arises when renaming things, especially when you rename something on the line defining the size of the indentation. Let's see what happens to this code when you rename the function:
```java
// before rename
public int addIntegers(int a,
                       int b) {
    return a + b;
}
// after rename
public int add(int a,
                       int b) {
    return a + b;
}
```
As you can see, this kind of continuation breaks easily when you rename a function, which probably happens a lot.

Both these problems can be fixed by choosing a continuation style that doesn't relate to previous lines. Here are some examples:
```java
// 1 tab worth
public int add(int a,
    int b) {
    return a + b;
}
// 2 tabs worth
public int add(int a,
        int b) {
    return a + b;
}
```
The first example lowers readability a little bit, but there are options to reduce this problem.

### Big blobs of code
Let's look at a more complicated function with multiple indentations and continuations:
```java
public Reservation makeReservation(String restaurantName, 
    DateTime time,
    int count) {
    Restaurant restaurant = restaurantService
        .getRestaurantByName(restaurantName);
    if (restaurant.isOpen() &&
        restaurant.reservationCount(time) + count < restaurant.capacity()) {
        Reservation reservation = restaurant.makeReservation(time, count);
        restaurantService.saveRestaurant(restaurant);
        return reservation;
    }
    return null;
}
```
This is already hard to read even though there are less than 10 lines of code. Here are some tips if you encounter this kind of code a lot: 
- use a bigger size for continuation lines: here the continuation lines are on the same indentation level as the code inside the block. It's hard to determine what's the continuation and what's the block. Using 2 tabs for continuation lines will mark a difference between the continuation and the block.
- add white lines/brace lines: whether you place your opening braces on the following line or use a white line to separate the continuations from the blocks it helps a lot to separate continuation from block.
- refactor some of it, combine shorter lines, extract functions: extracting multiple parts of code to functions is always a good idea to increase readability. Inverting conditionals often reduces complex reading also.

With these tips in mind, here is the same example:
```java
public Reservation makeReservation(String restaurantName,
        DateTime time,
        int count)
{
    Restaurant restaurant = restaurantService.getRestaurantByName(restaurantName);
    if (reservationCannotBeMade(restaurant, time, count))
        return null;

    Reservation reservation = restaurant.makeReservation(time, count);
    restaurantService.saveRestaurant(restaurant);
    return reservation;
}

private boolean reservationCannotBeMade(Restaurant restaurant, DateTime time, int count) 
{
    return restaurant.isClosed() ||
            restaurant.reservationCount(time) + count > restaurant.capacity();
}
```

### Working in a team
As I've stated in the past, if you're working in a team, the whole team should decide the style together and set it in their editors so that it's standard for everybody and you don't have unnecessary modified lines in code reviews. A great place to put these settings is in the .editorconfig file of your project. This file is read by most of the major editors, IDEs and even websites like Github and Bitbucket. Having the indentation style specified in this file helps to make the code in your Github code reviews the same as in your IDE.
