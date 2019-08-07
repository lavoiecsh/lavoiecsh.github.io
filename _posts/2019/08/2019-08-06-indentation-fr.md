---
layout: post
title: "Indentation"
lang: fr
ref: indentation
date: 2019-08-06
categories: [Pratiques]
tags: []
---
À travers les années, plusieurs styles d'indentation ont fait surface. De K&R à Allman, Whitesmiths ou GNU, utiliser des tabulations ou des espaces, il y a beaucoup de choix. Comment en choisir un?

Je ne vais pas vous dire quel style utiliser, mais vous donner des lignes directrices pour savoir comment en choisir un qui pourra vous servir longtemps et réduira les maux de têtes qui peuvent arriver avec des refactorings et autres.

### Pourquoi les tabulations et les espaces?
C'est un débat de longue date dans lequel il y a eu des arguments des deux côtés. Les arguments habituels pour les tabulations incluent:
- moins d'espace disque utilisé: puisque vous utilisez une tabulation au lieu de plusieurs espaces, à la longue ça peut sauver de l'espace disque. Ceci était un gros problème dans les premiers jours de la programmation, mais avec l'espace disque et mémoire que nous avous aujourd'hui, l'argument ne tient plus vraiment la route.
- permet une préférence de l'utilisateur: si vous préférez 4 espaces par tabulations et que votre collègue préfère 8, vous pouvez chacun définir la taille d'une tabulation et il ne devrait pas y avoir d'interférence entre vous deux. Faites attention d'utiliser un style de continuation qui permet ceci (j'y reviens sous peu).
De l'autre côté, les arguments pour les espaces sont généralement:
- standard universel: tout le monde voit le code de la même façon.
- ça permet des tabulations partielles: certains préfèrent ceci, mais comme je vais le démontrer bientôt, ce n'est pas un argument avec lequel je suis d'accord.

### Lignes de continuation
Un des plus gros problèmes d'indentation est les lignes de continuation. Deux problèmes peuvent survenir si vous alignez vos arguments ensemble. Premièrement, si vous utilisez des tabulations pour indenter (et possiblement des espaces pour corriger la dernière partie de l'indentation, souvent référé comme smart tabs), des tailles de tabulation différentes vont causer des problèmes:
```java
// avec tab-size = 4, 3 tabs + 3 caractères
public int add(int a,
               int b) {
    return a + b;
}
// avec tab-size = 8, 3 tabs + 3 caractères
public int add(int a,
                           int b) {
        return a + b;
}
```
La première version peut bien paraître, mais la seconde version devient déjà plus difficile à lire, même avec un exemple aussi simple.

Le second problème qui survient est lors du refactoring. Si vous renommez ou changez une partie de la ligne définissant la continuation, le reste ne suivra pas nécessairement:
```java
// avant de renommer
public int addIntegers(int a,
                       int b) {
    return a + b;
}
// après avoir renommé
public int add(int a,
                       int b) {
    return a + b;
}
```
Comme vous pouvez le constater, ce style de continuation brise facilement si vous renommez une fonction ou changez sa visibilité, ou son type de retour, ce qui arrive souvent.

Ces deux problèmes peuvent être résolus si vous choisissez un style de continuation qui ne se fie pas aux lignes précédentes. Voici quelques exemples:
```java
// 1 tabulation
public int add(int a,
    int b) {
    return a + b;
}
// 2 tabulations
public int add(int a,
        int b) {
    return a + b;
}
```
Le premier exemple réduit un peu la lisibilité, mais il y a des options pour réduire ce problème.

### Gros blocs de code
Regardons une fonction plus compliqué avec plusieurs indentations et continuations:
```java
public Reservation makeReservation(String restaurantName, 
    DateTime time,
    int count) {
    Restaurant restaurant = restaurantService
        .getRestaurantByName(restaurantName);
    if (restaurant.isOpen() &&
        restaurant.reservationCount(time) + count < restaurant.capacity()) {
        Reservation reservation = restaurant.makeReservation(time, count);
        restaurantService.saveRestaurant(restaurant);
        return reservation;
    }
    return null;
}
```
C'est déjà difficile à lire même s'il y a n'y a même pas 10 lignes de code. Voici quelques conseils si vous rencontrez souvent ce genre de code:
- utilisez une taille de tabulation plus grande pour les lignes de continuations: ici les lignes de continuation sont indentées au même niveaux que le code à l'intérieur du bloc. C'est difficile de déterminer qu'est-ce qui est continuation et qu'est-ce qui est bloc. Utilisez 2 tabulations pour les lignes de continuations aide à marker une différence entre les continuations et les blocs.
- ajoutez des lignes blanches ou des lignes d'accolades: si vous placez soit vos accolades ouvrantes sur la ligne suivante ou vous placez une ligne blanche pour séparer les continuations des blocs de code, ça aide à séparer les deux.
- refactorez une partie, combinez les lignes plus courtes, extrayez des fonctions: extraire plusieurs parties du code dans des fonctions est toujours une bonne idée pour augmenter la lisibilité. Inverser des conditions réduit souvent la complexité aussi.

Avec ces conseils en tête, voici le même exemple:
```java
public Reservation makeReservation(String restaurantName,
        DateTime time,
        int count)
{
    Restaurant restaurant = restaurantService.getRestaurantByName(restaurantName);
    if (reservationCannotBeMade(restaurant, time, count))
        return null;

    Reservation reservation = restaurant.makeReservation(time, count);
    restaurantService.saveRestaurant(restaurant);
    return reservation;
}

private boolean reservationCannotBeMade(Restaurant restaurant, DateTime time, int count) 
{
    return restaurant.isClosed() ||
            restaurant.reservationCount(time) + count > restaurant.capacity();
}
```

### Travailler en équipe
Comme je l'ai déjà mentionné dans le passé, si vous travaillez en équipe, toute l'équipe doit choisir un style ensemble et s'assurer que leurs éditeurs l'utilise pour que ça soit standard pour tout le monde et qu'il n'y ait pas de modifications de lignes inutiles dans les revues de code. Une bonne place pour mettre ces configurations est dans le fichier .editorconfig de votre projet. Ce fichier est lu par la majorité des éditeurs de textes, IDEs et même certains sites web comme Github et Bitbucket. Avoir un style d'indentation spécifié dans ce fichier aide à faire que le code dans les revues sur Github ressemble plus à votre IDE.
