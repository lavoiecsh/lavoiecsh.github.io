---
title: "Importance of Good Tools"
tags: [Practices]
---
There are a lot of tools to work with code now and it's getting harder and harder to choose between the different editors, IDEs, compilers, linkers, build engines, version control systems and such. How do you choose between all of them? I'll focus mainly on editors and IDEs for the moment might come back for other tools later on. My point here is not to tell you which editor or IDE to use or not, but help you choose the correct one for you.

<!-- truncate -->

### Solo or Team Work?
There are concerns when working in a team like having a unified code style that may make you tilt in a certain direction. Most editors and IDEs nowadays come with very complete code style engines to help you format the code the way you want it with as little effort as possible. The important part is that everybody in the team works with the same code style. This helps to reduce "useless" modifications in commits (where only the spacing, indentation or line wrapping changed) which will make it easier to review code. IDEs like the JetBrains suite or Visual Studio with ReSharper offer this out of the box and you can version the code style options so that everybody uses the same settings.  Most of the IDEs will only work if you use the same one (because their settings are saved with a custom format), but some IDEs will read a .editorconfig file containing the code style settings you need.

Check out what your coworkers are using: chances are they'll help you set it up and answer your questions also.

### What language are you working on?
Simpler editors handle most languages well, but don't handle any language exceptionally well (except the language in which it was written, or which was written for it like ELisp and VimScript). Having a more specialized IDE for your language will provide a lot of features that editors won't be able to provide (like running, debugging, templates, etc).

What kind of work are you going to do with the language? If it's a simple bash script you won't change for a while, most editors will do the job, but if you're going to work on a larger project for a couple of months, a more specialized IDE will greatly help you. More complete IDEs will often provide refactoring features that will help you write better and faster code.

That said, it's always a good idea to have a preferred editor for those times you need to quickly edit a file that's not well supported by your IDE of choice.

### Are you using weird key bindings?
This mostly applies to those coming from the olden days of Nano, Emacs and Vim. Back then key bindings weren't common among different editors and so each editor created their own set of key bindings. Nowadays, IDEs will most likely come with a simple set of key bindings but if you don't feel like learning them, you can look into which other key bindings the IDE can support.

I started coding using Emacs and have used it extensively for a couple of years of university. When I started working with others, I had to switch and this is a point that made me choose Rider over Visual Studio when working with C#. Visual Studio's support for Emacs key bindings stopped in 2010 and was added as a plugin which didn't work that well for the later versions, while all the JetBrains editors fully support Emacs key bindings (except C-t which has been in their backlog for a while now...). Since I still use Emacs as my main "quick" editor, it's nice not having to change between different key bindings for each editor I use.

### Paying a little extra might be a good option
Obviously when you're looking at fully integrated IDEs, most of them require you to pay a large amount to access a lot of the features. That cost is often easily repaid when you account for the amount of time you save doing a repeated task or writing better code. If you're not sure the cost is worth it, try it out first. JetBrains allows you to try out the next version of the IDE during it's Early Access Program, and offers Community or Student editions of a couple IDEs (just make sure you follow the constraints for these). Microsoft also has a Community edition for Visual Studio so you can try it out. Most of the time the feature differences are available on their website to better help you choose the version you need.

Of course, if you're not using the extra features, you can probably check out the free alternatives they have: Eclipse, NetBeans, Android Studio, Visual Studio Code, Atom (and I'm skipping some) or simply use your favourite editor.

### Conclusion
The IDE is the tool you're going to use most, so make sure you choose the best one for you needs. Try out different tools and check out new tools that come out when possible. Look for plugins that might add features to help you and make some if you feel like you're missing something.

P.S.: For those wondering, I've said it a little earlier, but my main tools these days are Intellij IDEA for Java and Android coding, Rider for C#, WebStorm for this blog and Emacs for pretty much all the rest.
