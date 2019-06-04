---
layout: post
title: "\"Service\" devrait être un mot bani"
lang: fr
ref: service
date: 2019-06-04
categories: [Pratiques]
tags: []
---
Tout comme "Manager" ou "Data", je crois que "Service" est un mot que nous devrions banir de notre code. Quand vous lisez la théorie pour le Domain Driven Design, les services sont là pour vous aider à correctement séparer vos logiques et contenir une partie de la logique d'affaire pendant qu'ils manipulent des objets du domaine. Malgré le fait que les services sont une partie intégrale du DDD, ils ne sont pas un patron de conception donc vous ne devriez pas avoir une classe dont le nom contient "Service".

### Ça mène à du code non maintenable
Nous avons une belle expression en français pour ça: "fourre-tout". Cette expression décrit bien ce qui arrive lorsque vous avez une classe "Service".

Disons que vous travaillez sur une application de marché en ligne. Vous aurez éventuellement un panier auquel vous pouvez ajouter des items. Votre premier réflexe est probablement de créer un CartService qui contiendra tout ce qui est relié au panier. Vous pouvez créer un nouveau panier, ajouter ou retirer un item du panier, modifier la quantité d'un item dans le panier, traiter le panier, supprimer le panier, etc. Déjà on voit six méthodes. Si vous moyennez environ 10 lines par méthode (incluant les fonctions privées nécessaires à chaque méthode), c'est déjà 60 lignes. Ajoutez les imports et les autres informations obligatoires, vous êtes probablement près des 100 lignes. Maintenant revenez à ces six méthodes: chaque méthode compte pour 10 pourcents du fichier entier. Si vous recherchez une information dans cette classe, 90 pourcents vous est inutile. Chaque fois que vous voudrez ajouter une nouvelle fonctionnalité, vous vous demanderez "où est-ce que je mets cette méthode?".

Maintenant regardez les tests pour cette classe. Six méthodes ayant environ cinq tests chaque: c'est déjà 30 tests unitaires, probablement tous dans le même fichier nommé "CartServiceTest". Ça devient très difficile à gérer.

Si vous utilisez le pattern repository incorrectement (voir ma [dernière entrée]({% post_url 2019-05-22-crudrepository-fr %})), vous auraz probablement des méthodes dans ce service pour traduire entre le panier du domaine et le panier de la base de données et appeler la méthode correspondante du repository. Toutes ces méthodes auront aussi un test ou deux. Ce CartService est rapidemment en train de grandir à une taille qui n'est plus maintenable. Il en va de même pour le CartServiceTest.

Un autre problème qui arrive avec des gros fichiers contenant tout est des problèmes de fusion (merge) lorsque vous travaillez avec plusieurs personnes. Deux récits peuvent vous forcer à travailler dans le même gros fichier alors qu'ils font références à des fonctionnalités différentes. Si deux personnes prennent un récit chaque, il risque d'y avoir des fusions à faire à un certain point.

### Comment est-ce que vous pouvez corriger ceci?

#### Assurez-vous de bien utiliser le patron repository
Il devrait recevoir et retourner des objets du domaine. Ceci permet de l'utiliser directement dans votre domaine, réduisant le besoin de méthodes de traduction dans vos services. Si vous n'avez pas de méthodes de traduction dans votre domaine, vous n'aurez donc pas non plus de tests pour celles-ci, réduisant les méthodes "utilitaires" et concentrant votre domaine sur les choses importantes.

#### Pensez Principe de Responsabilité Unique (Single Responsibility Principle)
Chaque classe devrait avoir une responsabilité et bien la faire. Séparez vos méthodes dans des classes différentes et nommez ces classes selon ce qu'elle font. Souvenez vous que chaque classe devrait être un nom et chaque méthode un verbe. En revenant à l'exemple du panier, vous pourriez avoir CartCreator.Create, CartItemAdder.Add, CartItemRemover.Remove, etc. Séparez aussi vos tests dans des classes différentes pour chaque fonctionnalité

### En quoi ceci corrige les choses?
Chaque nouvelle classe n'aura qu'une responsabilité, elle ne sera pas bourrée de méthodes non pertinentes à cette responsabilité, et les tests associés à cette fonctionnalité seront aussi séparés. Ce sera donc très facile de retrouver les règles d'affaires associées à une fonctionnalité: elles seront toutes dans un fichier de code et un fichier de test. Si vous trouvez que votre classe devient trop large parce que sa fonctionnalité associée contient beaucoup de règles d'affaire, rien ne vous empêche de les extraires dans une classe à part, ou de grouper des règles utilisées par plusieurs fonctionnalités dans des classes partagées. Au moins chaque fonctionnalité n'a qu'un point d'entrée et il est facile de le retrouver.

En ajoutant de plus en plus de fonctionnalités dans le futur, ce sera aussi plus facile de savoir où placer cette fonctionnalité (créez un fichier) ou trouver une fonctionnalité existante (avoir une bonne structure de fichier aide, mais les éditeurs peuvent facilement trouver un fichier dans votre projet complet en un instant).

Comme bonus, vous n'aurez plus de problèmes de fusion lorsque deux personnes travaillent sur des fonctionnalités différentes.
