---
layout: post
title: "Nouveautés dans C#8: Types de référence nullables"
lang: fr
ref: cs8_nullable_reference_types
date: 2019-03-19
categories: [Technologies]
tags: [C#]
---
Aujourd'hui je commence une courte série sur les nouvelles fonctionnalités qui viendront dans C#8.0, en commençant par les types de référence nullables.

C# a un système de types, comme plusieurs autres langages, qui vous permet d'assigner null à n'importe quel objet non-primitif (tout sauf les entiers, nombres à virgule et caractères). Ceci implique que lorsque vous recevez un type de référence comme paramêtre de fonction ou vous en conservez un comme variable d'instance, vous devez vous assurez qu'il n'est pas null avant de vous en servir (ou gérer le NullReferenceException qui va survenir avec son utilisation). Ceci veut aussi dire que vous ne pouviez pas avoir de types de référence nullables (ex: `string?` ou `Person?`).

Une des fonctionnalités qui s'en vient est un indicateur de compilation que vous pouvez ajouter à votre projet pour faire que les types de références requierent des valeurs non-null, tout en permettre de créer des types de référence nullables. Ceci explicite le fait que votre variable ou paramêtre peut être null et que vous allez le gérer.

Le seul problème présentement avec l'aperçu est que ceci n'est pas imposé au moment de la compilation, ça ne fait que montrer un avis que vous n'avez pas initialisé un type de référence non-nullable ou quand vous vous en servez:

```C#
class Person {
    private string firstName; // string non-nullable
    public string LastName { get; set; }
    
    public Person() {
        // ce constructeur a les avis suivants:
        // Non-nullable field 'firstName' is uninitialized.
        // Non-nullable property 'LastName' is uninitialized.
        
        // ceci ne devrait idéalement pas compiler ou initialiser le field et la property avec des valeurs par défaut
    }
    
    public Person(Person copy) {
        firstName = copy.firstName;
        LastName = copy.LastName;
    }
    
    public string FirstName() => firstName;
}
```

```C#
[TestClass]
class PersonTest {
    [TestMethod]
    public void CreatesPersonFromNothing() {
        var person = new Person();
        // ces deux assertions vont passer (quoique le field et la property devraient être non-null)
        // et montrer les avis suivants:
        // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
        Assert.AreEqual(null, person.FirstName());
        Assert.AreEqual(null, person.LastName);
        // ceci ne devrait pas passer parce que le field et la property ne peuvent pas être null
    }
    
    [TestMethod]
    public void ThrowsExceptionWhenCreatingPersonFromNull() {
        // ceci affiche l'avis: 
        // Cannot convert null literal to non-nullable reference or unconstrained type parameter.
        Assert.ThrowsException<NullReferenceException>(() => new Person(null));
        // ceci ne devrait pas compiler parce que vous passer null à un type de référence non-nullable
    }
}
```

Donc en tout et pour tout, j'aime bien cette addition mais je préférerais avoir des erreurs de compilation et des initialisations par défaut (string vide ou construteur sans paramêtres). Ceci forcerait à toujours avoir une valeur, soit en l'initialisant pour vous dans le cas des fields et properties, soit en vous forçant à passer une valeur non-null comme paramêtre de fonction. Avoir l'indicateur pour la compilation implique qu'on ne va pas coder défensivement (vérifier que nos objets ne sont pas null avant de les utiliser), il faudrait donc un moyen de s'assurer que l'objet que nous utilisons n'est pas null à la compilation.