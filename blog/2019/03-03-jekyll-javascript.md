---
title: "Jekyll, pagination, categorizing and Javascript"
tags: [Blog,Update]
---
As some of you may know Jekyll is an application to build static websites. It's used by GitHub-Pages (where this blog is residing). One of the disadvantages of having a static website is that every page must be created before publishing it. This means that you cannot do filtering based on query parameters and such, and all the sub-pages for pagination must be generated before hand. Jekyll does allow extensions in Ruby to generate the pages, one of which is a paginator, but for some reason I've had trouble making it work on GitHub-Pages. The paginator also doesn't work with multiple languages like this blog.

After trying all this for a while, I decided to go the old route and add a bit of Javascript to my pages to add pagination and categorizing. I used ES6 Javascript with no additional libraries (like JQuery).

### Categorizing

Categorizing has been achieved by removing posts from the list if they don't fit the currently selected. I added a query parameter to the Posts by category links on the left of the page and retrieve it with the following Javascript code:

```javascript
const categories = window.location.search.substr(1).split('&').map(v => v.split('='));
return new Map(categories).get('category');
```

Filtering is then accomplished with this bit of code:

```javascript
let postList = document.querySelector('ul.post-list');
document.querySelectorAll('ul.post-list li')
    .forEach(post => post.dataset.category !== category && postList.removeChild(post));
```

### Pagination

Once this is done I add pagination by creating li and button elements in an empty ul already in the page. Page selection is then accomplished by hiding the posts from other pages while showing the posts from the current page. Since the code is a little larger, I'll simply post a link to it [here](/js/paginator.js).

### Disclaimer

I am aware this will not work in older browsers or with Javascript disabled, but I'm guessing people that read this blog will be using a recent browser anyway. Not running the Javascript on the index page will simply mean that categorizing and pagination will not work, but the actual website will still continue working as it should.

### So what's left after this update?

Fixing dates so they show according to the language (which seems to require a lot of code). More CSS everywhere.
