---
layout: post
title: "Critique de livre: Gang of Four"
lang: fr
ref: gangoffour
date: 2018-11-06 9:00:00 -0400
categories: [Revue]
tags: []
---
Je commence cette série de revue de livres par le fameux livre à propos des patrons de conception. Pour ceux qui ne savent pas de quel livre je parle: *Design Patterns: Elements of Reusable Object-Oriented Software*, écrit par Erich Gamma, John Vlissides, Ralph Johnson, et Richard Helm.

Ce livre a souvent été considéré comme une référence que tout le monde devrait lire (et je me compte dans cette catégorie). Je crois par contre que le livre n'a pas besoin d'être lu dans son entièreté pour que le lecture puisse utiliser ses concepts.

### Pour qui est ce livre?
Les développeurs en orienté-objet. Ce livre a été écrit pour aider à résoudre des problèmes que l'on retrouve généralement lorsque nous développons en OO (principalement Java, C++, C#, Ruby, etc). La majorité des patrons sont soit déjà implémentés ou soit non-nécessaires dans des languages fonctionnels et/ou procéduraux.

### Qu'est-ce qui devrait être lu dans ce livre?
Les principales sections à lire sont les sections "Intent", "Motivation", "Applicability" et "Structure" pour chaque patron. Je crois que c'est un suffisamment petit sous-ensemble de l'information pour savoir quand utiliser un patron et comment le reconnaître. L'introduction et l'exemple de cas peuvent aider les développeurs plus novices.

### Qu'est-ce qui devrait être retenu de ce livre?
Les patrons de conception existent et sont très utiles pour simplifier le code et permettre à un logiciel de grossir sans que le code ne deviennent trop complexe. Ce livre est aussi présenté comme une référence qui peut être utilisée lorsqu'on a besoin d'implémenter ou comprendre un patron.

### Comment est-ce que le code propre s'applique aux patrons de conception?
Les patrons de conception aident grandement à rendre votre code plus facile à comprendre plus tard et ajoutent beaucoup de flexibilité lorsque vient le temps d'effectuer des changements dans l'application. Une chose à se rappeler lorsqu'on implémente des patrons de conception est qu'il est généralement bien vu de nommer les classes participantes selon le patron. Par exemple, `EmployeeEndpointLoggingDecorator` pour un décorateur effectuant du logging autour du endpoint d'employés, ou `EmployeeSubject` et `EmployeeObserver` lorsqu'on implémente le patron observateur. Ceci facilite la reconnaissance du patron quand vous ou quelqu'un d'autre retournera au code un an plus tard.
