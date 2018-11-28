---
layout: post
title: "Book Review: Clean Architecture"
lang: en
ref: cleanarchitecture
date: 2018-11-20 9:00:00 -0400
categories: [reviews]
tags: [reviews]
---
Next up in the book review series: *Clean Architecture: A Craftsman's Guide to Software Structure and Design* by Robert Martin. Although I've loved a lot of the other books by Uncle Bob, I have to admit this one disappointed me and I'll explain why in this book review.

The book contains 3 main sections:
1. A history of software development (parts I and II of the book)
2. Principles for software development (parts III and IV of the book)
3. Guidelines for software architecture (parts V and VI of the book)

The first section is interesting for cultural knowledge, but not very informative otherwise. It describes some of the advances that made languages what they are today and it explains the reasoning behind a lot of the constraints we have today.

The second section is the "theoretical" part of the book. The first five chapters describe SOLID principles, which we have all read about in a lot of other books, and the last three describe similar principles defined for components (instead of classes and interfaces). These are important to understand and is the main part of the book.

The third section is an "application" of the principles describes in the second section. I found a lot of the examples repeated a lot of the information, especially the Details section (part VI). I also think the examples weren't complete in the sense that they don't really present the solution, only the problem. The last chapter (appropriately named "The Missing Chapter" as it was written by Simon Brown as what seems to be an attempt to fix the book) presents the best example and describes the options and decisions that would happen in a real world case.

### Who is this book for?
I'm still wondering how to answer this question. As described above, the book presents a lot of useless and/or known information to most advanced developers while presenting new information that would be too advanced for novice developers.

### What should be read in this book?
If you consider yourself a novice developer, and have never had to work with multiple projects/assemblies/modules: the beginning of the book is excellent for you but you might have trouble understanding the component principles and guidelines, a good challenge nonetheless. If you've worked with bigger projects and would like to understand how other developers/architects in your development teams think: start at the component principles (part IV).

### What should be retained from this book?
Architecture is about making the least amount of concrete decisions that would force you in a corner, or about making the most amount of abstract decisions that leave options available, however you want to read it. Some decisions will facilitate development but make deployment harder, others will do the opposite. The component principles help you put SOLID principles (pun intended) around larger groupings of objects to help guide those decisions. Each project is different and must be treated differently in regards to it's architecture.

### How does proper code apply to clean architecture?
Clean Architecture was derived from Uncle Bob's Clean Code and as such is very relevant in writing proper code. Just like design patterns help make changes and additions to a subset of objects easier down the line, software architecture should help make changes and additions to the whole system easier down the line.