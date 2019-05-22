---
layout: post
title: "CrudRepository est un anti-patron"
lang: fr
ref: crudrepository
date: 2019-05-22
categories: [Pratiques]
tags: []
---
Les CrudRepositories peuvent sembler comme une belle façon d'augmenter la productivité, mais je crois qu'ils mènent à une mauvaise architecture et je vais expliquer pourquoi.

### Premièrement, qu'est-ce qu'un CrudRepository?
Dans le domain driven design, un repository est un objet utilisé pour accéder à de l'information persistée, habituellement dans une base de donnée. Il procure des méthodes pour accéder certain objets et les modifier. CRUD est le plus petit ensemble de méthodes qui peuvent être utilisé pour effectuer tout le travail requis sur vos données. Il consiste en quatre méthode nommées: Create, Read, Update, Delete. Un CrudRepository est donc une classe qui implémente ces quatres méthodes. Ces objets sont généralement génériques, et peuvent même être automatiquement générés (voir Spring). Ils ressemblent habituellement à ceci:
```java
public class CrudRepository<T> {
    public int create(T t) {...}
    public T read(int id) {...}
    public void update(T t) {...}
    public void delete(T t) {...}
}
```

### Ils mènent à une architecture en couche
Donc le premier problème que je vois est qu'ils mènent à une architecture en couche dans le sens que vos objets du domaine vont dépendre des objets de la base de données. La classe que vous passez en paramêtre à ce CrudRepository va contenir de l'information par rapport à la base de données, que ce soit par des annotations ou par le fait que votre classe représente exactement une table spécifique. Deux choses peuvent arriver lorsque vous commencez à utiliser ce patron:
1. Vous utilisez des objets du domaine, mais vos services du domaine doivent maintenant traduire vos objets du domaine en objets de la base de données et vice-versa quand il utilise le repository. Ceci ajoute une responsabilité de plus à vos services, les rendant plus difficiles à lire.
2. Vous allez simplement utiliser les objets de la base de données partout dans votre application, vous forçant à écrire des services qui vont contenir toute la logique du domaine et avoir des objets qui ne contiendront aucune logique.

Un autre gros point à noter avec l'architecture en couche est que lorsqu'il y a des changements dans le schéma de la base de données, vous allez probablement devoir changer des bouts de codes un peu partout dans votre application. Ceci devient de plus en plus compliqué lorsque votre application grandit et va vous prendre de plus en plus de temps.

Le design guidé par le domaine encourage une séparation claire entre les objets du domaine et votre base de données pour réduire ce problème. Vous devriez avoir une interface du repository qui réside dans votre domaine et parle en terme d'objets du domaine. L'implémentation de cette interface sera dans ailleurs et s'occupera de la traduction entre les objets du domaine et les objets de la base de données. Un changement à la base de données affectera seulement les objets de la base de données et l'implémentation des repositories, rien de plus.

### Comment pouvez-vous remédier à ceci?
Comme mentionné plus haut, vous devriez avoir une interface pour votre repository dans votre domaine qui utilise des objets du domaine. Quelque chose que ceci:
```java
public interface EmployeeRepository {
    public Employee createNewEmployee(String firstName, String lastName, String email, ...);
    public Employee getEmployeeById(int id);
    public Employee getEmployeeByEmail(String email);
    public void saveEmployee(Employee employee);
    public void deleteEmployee(Employee employee);
}
```

Votre implémentation va donc faire la traduction entre les objets du domaine (Employee dans ce cas) et vos objets de base de données. Cette classe peut utiliser le CrudRepository si vous voulez:
```java
public class SqlEmployeeRepository : EmployeeRepository {
    private CrudRepository<DbEmployee> crud;
    
    public Employee createNewEmployee(String firstName, String lastName, String email, ...) {
        Employee employee = new Employee(firstName, lastName, email);
        DbEmployee dbEmployee = new DbEmployee(employee);
        employee.id = crud.create(dbEmployee);
        return employee;
    }
    
    public getEmployeeById(int id) {
        return crud.read(id).toEmployee();
    }
    
    public void saveEmployee(Employee employee) {
        crud.update(new DbEmployee(employee));
    }
    
    public void deleteEmployee(Employee employee) {
        crud.delete(new DbEmployee(employee))
    }
    
    ...
}
```

Maintenant, dès que vous avez des changements dans votre schéma de base de données, vous allez seulement modifier les objets de base de données (DbEmployee) et l'implémentation de votre repository (SqlEmployeeRepository). Si votre domaine n'a pas besoin de cette information ajoutée, vous pouvez l'ignorer. Ceci est particulièrement utile quand vous travaillez avec une base de données partagée à travers plusieurs application et que vous n'utilisez qu'une partie de ce qui y est sauvegardée. Si l'information ne vous est pas utile, alors pourquoi vous forcer à l'intégrer dans votre domaine? Créez des objets du domaine qui contiennent seulement l'information que vous avez besoin pour votre application et laissez les repositories et les objets de base de données extraire l'information pertinente de la base de données.

Avoir cette traduction entre le domaine et la base de données dans vos repositories simplifie aussi vos services du domaine, les rendant plus lisible et donc plus facile à modifier pour ajouter des nouvelles fonctionnalités ou en modifier des existantes, tout en réduisant le risque d'erreur.

### Donc pourquoi s'en aller des repositories crud si on doit maintenant travailler plus?
Vous allez plus facilement séparer les différents types de logique entre chaque couche de votre application et ce sera plus facile de faire des modifications et de comprendre vos objets du domaine et vos services sur le long terme.
Prenez par exemple ces deux versions du même service pour engager un nouvel employé:
```java
public EmployeeService {
    private CrudRepository<EmployeeEntity> repository;
    private EmployeeEmailService emailService;
    
    public EmployeeBean hireEmployee(String firstName, String lastName) {
        String email = emailService.generateEmailForNewEmployee(firstName, lastName);
        EmployeeEntity entity = new EmployeeEntity(firstName, lastName, email);
        entity.id = repository.create(entity);
        return new EmployeeBean.Builder()
            .id(entity.id)
            .firstName(entity.firstName)
            .lastName(entity.lastName)
            .email(entity.email);
    }
}
```

```java
public EmployeeService {
    private EmployeeRepository repository;
    private EmployeeEmailService emailService;
    
    public Employee hireEmployee(String firstName, String lastName) {
        String email = emailService.generateEmailForNewEmployee(firstName, lastName);
        return repository.createNewEmployee(firstName, lastName, email);
    }
}
```

Dans le premier cas, utilisant un repository crud, vous polluez le service avec de la traduction entre les différents types d'objets représentant essentiellement le même concept (un employé). Seulement deux des huit lignes sont pertinentes et sont perdues dans la traduction. Dans le second exemple, les deux mêmes lignes importantes sont les seules et c'est donc beaucoup plus clair et facile à lire de cette façon.

Un autre avantage est lorsque vous écrivez des tests unitaires pour ces méthodes. Dans le premier exemple, vous êtes obligés de comparer un EmployeeBean retourné par votre méthode avec un EmployeeEntity que vous avez retourné de votre repository mocké. Si vous ajoutez ensuite un champ dans EmployeeBean ou EmployeeEntity, you pourriez manquer cette traduction, mais vos tests vous quand même passer, causant probablement une erreur plus tard. Dans le second exemple, vous pouvez directement comparer l'objet retourné de votre repository mocké avec l'objet retourné par la méthode (ça devrait être le même), votre comparaison ne peut pas briser malgré des nouveaux champs dans la base de données.

### Conclusion
Souvenez-vous qu'une interface appartient à son utilisateur, pas son implémentation. Puisque l'utilisateur de votre repository est votre service, l'interface du repository appartient au service et devrait donc parler en termes de domaine, simplifiant le service qui l'utilise.
