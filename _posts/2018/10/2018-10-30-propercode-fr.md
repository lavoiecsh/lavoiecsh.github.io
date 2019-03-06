---
layout: post
title: "Proper Code"
lang: fr
ref: propercode
date: 2018-10-30 9:00:00 -0400
categories: [Mise-à-jour]
tags: []
---
J'ai commencé ma carrière de développement dans une grande entreprise en cascade à travailler sur leur logiciel en C++. Nous avions plusieurs couches de documents d'analyse décrivant le travail à effectuer pour implémenter chacune des fonctionnalités. Il n'y avait pas de tests. Le logiciel était si massif que la version actuelle de Visual Studio ne pouvait pas le supporter. La compilation prenait tellement de temps (plus de six heures pour compiler le système complet) que nous avions des compilations partielles qui référaient à une compilation de nuit et c'était assez fréquent d'avoir des compilations de plus d'une heure malgré celà. Lancer le logiciel pour tester les fonctionnalités prenait souvent plus de 5 minutes. Il fallait souvent travailler sur deux fonctionnalités en parallèle parce que nous étions toujours en train d'attendre et nous travaillions généralement seul sur une fonctionnalité pour des semaines et parfois des mois de temps. La compagnie engageaient des développeurs par cohortes de 20 à 30 et leur donnaient deux semaines de cours en partant. Le *refactoring* était hors de question parce que nous ne pouvions pas livrer les fonctionnalités assez vite.

J'ai ensuite changé pour une compagnie plus petite qui travaille en agile. J'ai appris à ne pas écrire des documents d'analyses, les *reviews*, rétros et *plannings* de *sprint*, les tests unitaires, le *refactoring*, les cycles de compilation et exécution en terme de secondes au lieu de minutes. Ils m'ont aussi appris à propos de Uncle Bob et Clean Code et m'ont même offert d'assister à une de ses formations.

J'ai été surpris à quel point il avait raison à propos des grosses compagnies car c'était exactement comme je l'avais vécu auparavant. Ça m'a encouragé à continuer d'apprendre davantage sur le clean code, le développement dirigé par les tests (TDD), le design axé sur le domaine (DDD), l'architecture et à transférer mes connaissances aux autres autours de moi. Ça a commencé par des petites séances avec des collègues, et c'est en train d'évoluer en ce blog.

Donc qu'est-ce que *Proper Code*? C'est tout ce qui entoure le clean code, TDD, DDD, l'architecture. Comment coder proprement pour que ce soit facile de faire des changements plus tard sans se ralentir au fur et à mesure que le logiciel grandi. C'est aussi les meilleures pratiques des langages et outils que nous utilisons et savoir les nouveaux développements et nouvelles fonctionnalités pour ces langages et outils.
