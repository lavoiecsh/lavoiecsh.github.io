---
layout: post
title: "Book Review: Gang of Four"
date: 2018-11-06 9:00:00 -0400
categories: [Review]
tags: []
---
I'm starting this book review series with the infamous book about design patterns. For those who don't know which book I'm talking about: *Design Patterns: Elements of Reusable Object-Oriented Software*, written by Erich Gamma, John Vlissides, Ralph Johnson, and Richard Helm.

This book has been considered by many as a reference that everybody should read (and I count myself in these as well). I do think the book does not need to be read in it's entirety for the reader to use it's concepts.

### Who is this book for?
Object-oriented software developers. This book was written to help solve problems generally found when developing OO software (mostly in Java, C++, C#, Ruby, etc.). Most of the patterns are either already implemented or not necessary in functional and/or procedural languages.

### What should be read in this book?
The main thing to read is the "Intent", "Motivation", "Applicability" and "Structure" sections of each pattern. I think this is a sufficiently small subset of information which you can use to determine when you should apply a pattern and how to recognize it in existing code. The introduction and case studies can also help more novice programmers.

### What should be retained from this book?
Design patterns exist and are very useful in simplifying software and allowing software to grow larger without making a mess. This book is also meant to be a reference which can be opened whenever the need to implement or understand a pattern arises.

### How does proper code apply to design patterns?
Design patterns greatly help in making your code easier to understand later on and adds a lot of flexibility when you need to make changes to parts of the application. One thing to remember when implementing design patterns is that it is usually a good idea to name participant classes according to the pattern. For example, `EmployeeEndpointLoggingDecorator` for a logging decorator around the employee endpoint, or `EmployeeSubject` and `EmployeeObserver` when implementing the observer pattern. This makes it easier to recognize the pattern when you or someone else returns to the code a year later.
