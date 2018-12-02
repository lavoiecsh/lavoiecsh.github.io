---
layout: post
title: "Day 1: Chronal Calibration"
lang: en
ref: chronal_calibration
date: 2018-12-01 9:00:00 -0400
categories: [advent]
tags: [advent]
---
The first day of the advent of code challenge starts off pretty slowly. You basically just need to sum the numbers up. I've done this by just replacing the end of lines by a space and surrounding it in a LISP sum expression ```(+ ...)```.

The second part required more work though, so I wrote a simple program to solve it. Fortunately, we are given a couple of examples that we can use as unit test cases.

I ended up creating a program in C# to do this using a simple loop. I also used this occasion to setup a couple of classes that I will probably be using for future problems, like a simple command line interface to all the solvers as well as a file reader to return all the integers in the file as a list.

I also added the first part of the problem in the solution.

All the code is available in my github repository: [advent solutions code](https://github.com/lavoiecsh/lavoiecsh.github.io/tree/master/advent).