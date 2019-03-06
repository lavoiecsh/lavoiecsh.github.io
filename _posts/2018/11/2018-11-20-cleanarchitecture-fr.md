---
layout: post
title: "Revue de livre: Clean Architecture"
lang: fr
ref: cleanarchitecture
date: 2018-11-20 9:00:00 -0400
categories: [Revue]
tags: []
---
Le prochain livre dans la série des revues: *Clean Architecture: A Craftsman's Guide to Software Structure and Design* par Robert Martin. Malgré le fait que j'ai aimé beaucoup de ces autres livres, je dois admettre que je suis déçu de celui-ci et je vais donner les raisons dans cette revue.

Le livre est séparé en trois grandes sections:
1. Un historique du développement logiciel (parties I et II du livre)
2. Les principes pour le développement logiciel (parties III et IV du livre)
3. Des lignes directrices pour le développement logiciel (parties V et VI du livre)

La première section est intéressante à titre de culture générale, mais pas très informative autrement. Elle décrit quelques avancées qui a fait que les langages de programmation sont ce qu'ils sont aujourd'hui et elle décrit le raisonnement derrière beaucoup des contraintes que nous avons maintenant.

La deuxième section est la partie "théorique" du livre. Les cinq premiers chapitres décrivent les principes SOLID, que nous avons tous lu dans plusieurs autres livres, et les trois derniers décrivent des principes similaires pour les composantes (au lieu des classes et interfaces). Ces principes sont importants à comprendre et sont le focus du livre.

La troisième section est une "application" des principes décrits à la deuxième section. J'ai trouvé que beaucoup des exemples répètent de l'information, surtout dans la partie *Details* (partie VI). J'ai aussi trouvé que beaucoup des exemples n'étaient pas complets dans le sens qu'ils ne présentent pas vraiment de solutions, seulement le problème. Le dernier chapitre (nommé "The Missing Chapter" car c'est Simon Brown qui l'a écrit pour ce qui semble être un essai à corriger le livre) présente le meilleur exemple et décrit les options et décisions qui arriveraient dans un example tiré de la vraie vie.

### Pour qui est ce livre?
Je suis encore en train de me demander comment répondre à cette question. Tel que décrit plus haut, le livre présente beaucoup d'informations inutiles et/ou déjà connues pour la plupart des développeurs avancés tout en présentant de l'information qui serait trop avancée pour des nouveaux développeurs.

### Qu'est-ce qui devrait être lu dans le livre?
Si vous vous considérez comme un nouveau développeur, et que vous n'avez jamais eu à travailler avec plusieurs projets/assemblies/modules: le début du livre est excellent pour vous, mais vous allez peut-être avoir de la difficulté à comprendre les principes des composantes et les lignes directrices, un bon challenge quand même. Si vous avez déjà travaillé sur des plus gros projets et aimeriez savoir comment les autres développeurs/architectes autour de vous pensent: commencez à la section sur les principes des composantes (partie IV).

### Qu'est-ce qui devrait être retenu de ce livre?
L'architecture est une discipline visant à faire le moins de décisions concrètes qui vous forceraient dans un coin, ou visant à faire le plus de décisions abstraites qui vous laisserons le plus d'options disponible, selon votre préférence. Certaines décisions facilitent le développement mais rendent le déploiement plus difficile, d'autres font l'inverse. Les principes des composantes vous aident à mettre des principes SOLID autour d'assemblages d'objets plus gros et vous guident dans ces décisions. Chaque projet est différent et devrait être traité de la sorte par rapport à son architecture.

### Comment est-ce que le code propre s'applique à clean architecture?
Clean Architecture a été dérivé de Clean Code par Uncle Bob et est donc pertinent quand on écrit du code propre. Tout comme les patrons de conception facilitent les changements et les additions à un petit sous-groupe d'objets, l'architecture devrait faciliter les changements et les additions dans une application complète.