---
layout: post
title: "Mise à jour sur la disposition"
lang: fr
ref: layout_update
date: 2019-01-17 17:57:00 -0400
categories: [Mise-à-jour]
tags: []
---
J'ai récemment décidé de mettre à jour mon site web et j'ai profité de cette occasion pour apprendre une nouvelle (quoique vieille) technologie: CSS Grid.

J'ai complètement retiré l'ancien CSS (incluant bootstrap) et réécrit tout moi-même. J'ai été surpris de voir ce que je pouvais faire avec si peu de lignes de SCSS puisque j'étais habitué à travailler avec bootstrap et d'autres librairies du genre qui ajoutent des milliers de lignes de code. Mon fichier SCSS courant fait moins de 150 lignes et n'inclut aucun autre fichier CSS.

Placer les objets dans la page avec CSS Grid est un pur bonheur comparé à essayer de travailler avec différentes tailles d'écrans en utilisant bootstrap et je recommanderais toujours d'utiliser CSS Grid en commençant un nouveau projet, surtout s'il va être utilisé sur plusieurs tailles d'écrans différentes.

J'ai encore du travail à faire, sur lequel je vais travailler dans les prochaines semaines, en ajoutant des mises à jour comme celle-ci et peut-être quelques leçons que j'ai appris en cours de route:
1. Filtrer les entrées de blog par catégorie (avec les liens déjà présents dans le menu).
2. Ajouter de la pagination pour la liste des entrées de blog pour que ça ne s'étende pas à l'infini.
3. Ajouter plus de CSS pour les entrées elles-mêmes (code dans la ligne, blocs de code, listes, etc).