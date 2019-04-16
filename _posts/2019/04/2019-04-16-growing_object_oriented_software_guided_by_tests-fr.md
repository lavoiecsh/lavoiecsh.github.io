---
layout: post
title: "Revue de livre: Growing Object-Oriented Software, Guided by Tests"
lang: fr
ref: growing_object_oriented_software_guided_by_tests
date: 2019-04-16
categories: [Revue]
tags: []
---
Aujourd'hui je vais faire une revue du livre "Growing Object-Oriented Software, Guided by Tests" de Steve Freeman et Nat Pryce.

Beaucoup de gens sont au courant du cycle de TDD de Fail - Pass - Refactor, ce livre utilise ce concept à un plus haut niveau en faisant des tests d'acceptation en utilisant ce cycle, ce qui force des cycles plus petits pour les tests d'intégration et unitaires. Il y a un très bon exemple qui aide à bien comprendre comment les différents concepts fonctionnent ensemble à une plus grande échelle.

### Pour qui est ce livre?
Les gens qui ont déjà une compréhension de base du développement piloté par les tests peuvent trouver des nouvelles possibilités dans l'écriture de tests d'acceptation qui vont piloter tous les autres tests et le code. Le livre explique aussi brièvement les concepts de base du TDD, donc les gens qui n'y sont pas habitué peuvent facilement le lire aussi.

### Qu'est-ce qui devrait être lu dans ce livre?
Je crois que ce livre devrait être lu en entier. Les concepts de base au début du livre sont très théoriques mais bien expliqués et l'exemple aide beaucoup à cimenter votre compréhension.

### Qu'est-ce qui devrait être retenu de ce livre?
Tous les concepts du développement piloté par les tests d'acceptation et le cycle de test plus grand. Il y a par contre une petite section au début de l'exemple que je ne recommande pas de retenir: le squelette marchant (walking skeleton).

Cette technique est utilisé pour démarrer un nouveau projet et consiste en écrire un test qui va force à implémenter une platforme de test et d'intégration continue. Par contre, de nos jours, avec la venue de platformes qui gèrent une grande partie de ça pour vous comme les pipelines de Bitbucket ou Azure DevOps de Microsoft (anciennement VSTS), cette pratique n'est plus aussi nécessaire qu'elle l'était autrefois (le livre a été écrit en 2009, les outils ont commencé à sortir vers 2017). La raison était que ces outils vous permettre de rapidement démarrer un nouveau projet avec de l'intégration continue avec un click de souris.

Je recommende quand même de commencer les projets par un test simple qui fait seulement lancer l'application et s'assure que vos librairies de test sont capables d'intéragir avec.

### Comment est-ce que ceci s'applique au code propre?
Le développement piloté par les tests est un concept central du code propre qui vous aide à vous assurer que la fonctionnalité est complète, découpler vos objets et écrire du code maintenable. En ajoutant les tests d'acceptation dans le cycle, ça facilite l'intégration de tests d'acceptation puisque vous les écrivez en premier. Ça assure aussi que vous n'oubliez pas une partie du code requise pour la nouvelle fonctionnalité. En utilisant Git Flow pour gérer vos branches de code source, c'est particulièrement puissant car la première chose que vous faites dans la nouvelle branche est un test d'acceptation, et votre branche est prête à être intégrée quand ce test passe. Il est à noter que cette technique peut aussi être utilisée pour corriger des bogues en écrivant un test qui suit le comportement attendu de l'application (qui devrait échouer parce qu'il y a un bogue) et ajouter des tests et du code jusqu'à ce que ce test passe (ce qui confirmera la correction du bogue).