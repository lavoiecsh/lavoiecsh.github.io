---
layout: post
title: "Jekyll, pagination, catégorisation et Javascript"
lang: fr
ref: jekyll-javascript
date: 2019-03-03
categories: [Mise-à-jour]
tags: []
---
Comme certains d'entre vous le savent Jekyll est une application pour construire des sites web statiques. C'est utilisé dans GitHub-Pages (où ce blog est présentement). Un des désavantages d'avoir un site web statique est que toutes les pages doivent être créées avant d'être publiées. Ceci imqlique qu'on ne peut pas faire de filtrage basé sur les paramêtres de la requête et toutes les sous-pages pour la pagination doivent être générées à l'avance. Jekyll permet des extensions en Ruby pour générer les pages, dont une pour paginer, mais pour une raison que j'ignore encore, je n'arrive pas à les faire fonctionner sur GitHub-Pages. L'extension de pagination ne fonctionne pas avec des sites qui ont plusieures langues comme celui-ni non plus.

Après avoir essayé ceci pendant un petit bout, j'ai décidé d'aller avec la vieille méthode et ajouter un peu de Javascript dans mes pages pour ajouter ces fonctionnalités. J'ai utilisé Javascript ES6 avec aucune autre librairie (comme JQuery).

### Catégorisation

La catégorisation se fait en retirant les entrées de la liste qui ne sont pas de la catégorie sélectionnée. J'ai ajouté un paramêtre de requête au liens à gauche pour le filtrage par catégorie et je vais le chercher avec le code Javascript suivant:

```javascript
const categories = window.location.search.substr(1).split('&').map(v => v.split('='));
return new Map(categories).get('category');
```

Le filtrage est accompli avec ce bout de code:

```javascript
let postList = document.querySelector('ul.post-list');
document.querySelectorAll('ul.post-list li')
    .forEach(post => post.dataset.category !== category && postList.removeChild(post));
```

### Pagination

Une fois que la catégorisation est faite, j'ajoute la pagination en créant des éléments de listes et des boutons dans une ul vide déjà dans la page. La sélection de la page cache les entrées des autres pages et affiche les entrées de la page courante. Puisque ce code est un peu plus long, je vais simplement ajouter un lien [ici](/js/paginator.js).

### Avertissement

Je suis au courant que ceci ne fonctionera pas pour les vieux fureteurs ou lorsque Javascript est désactivé, mais je me doute que les gens qui lisent ce blogue utilisent un fureteur récent de toute façon. De toute façon, ne pas rouler le Javascript ne brise pas le site, seul la catégorisation et la pagination ne fonctionneront pas.

### Qu'est-ce qu'il reste à faire après cette mise-à-jour?

Corriger les dates pour les afficher selon la langue (qui semble demander beaucoup de code). Plus de CSS un peu partout.