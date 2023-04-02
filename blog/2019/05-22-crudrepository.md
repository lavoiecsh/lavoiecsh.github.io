---
title: "CrudRepository is an anti-pattern"
tags: [Practices]
---
CrudRepositories may seem like a nice way to boost productivity, but I think they lead to bad design and I'll explain why.

### First off, what is a CrudRepository?
In Domain Driven Design, a repository is an object used to access persisted information, usually in a database. It provides methods for accessing certain objects and modifying them. CRUD is the smallest set of methods that can be used to work with your data. It consists of four explicitly named methods: Create, Read, Update, Delete. A CrudRepository is a class implementing these four methods. These are usually generic, and sometimes even automatically generated (see Spring). They usually look something like this:
```java
public class CrudRepository<T> {
   public int create(T t) {...}
   public T read(int id) {...}
   public void update(T t) {...}
   public void delete(T t) {...}
}
```

### It leads to layered architecture
So the first problem I see with this is that it will usually lead to a layered architecture in the sense that your domain objects will now depend on database objects. The class your passing to this CrudRepository will contain information that pertains to the database, either through annotations or by the class being a direct mapping of a specific table. One of two things will usually happen once you start using this pattern: 
1. You might use domain objects, but your domain services will have to translate those into database objects and vice-versa to use the repository. This adds an extra responsibility to your domain services, making them harder to read.
2. You will simply use those database objects everywhere in your application, forcing your to write your services to contain all the domain logic and not having domain objects that contain their specific logic.

Another big point to note with layered architecture is that whenever you change your database schema, you will likely have to change bits and pieces in all of your code. This will become harder and harder going further and take more and more time to complete.

Domain Driven Design encourages a clean separation between your domain objects and your database to reduce this problem. You should have a repository interface that resides within your domain and uses domain objects and the implementation should handle the translation between the domain objects and the database objects. A database change will then only affect your database objects and implementation of the repositories, but nothing else.

### How can you fix this?
As mentioned earlier, you should have a repository interface inside your domain that uses domain objects. Something like this:
```java
public interface EmployeeRepository {
    public Employee createNewEmployee(String firstName, String lastName, String email, ...);
    public Employee getEmployeeById(int id);
    public Employee getEmployeeByEmail(String email);
    public void saveEmployee(Employee employee);
    public void deleteEmployee(Employee employee);
}
```

Your implementation will then have to translate between your domain objects (Employee in this case) and your database objects. This class can use the CrudRepository if you want to:
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

Now, whenever you change the database schema, you only need to change the database object (DbEmployee) and the implementation of the repository (SqlEmployeeRepository). If your domain doesn't need this added information, you can just ignore it. This is especially useful when working with a database that may be used by other application within your company. You probably don't care about half of the information in the database, so why force yourself to map it exactly? Create domain objects that contain just the information you need for your application and let the repository and database objects extract the pertinent information from the database.

Having the translation between the domain objects and the database objects in the repository also simplifies your domain services, making it easier to add new features or modify existing ones, and reducing the risk of errors in these objects.

### So why move away from crud repositories if you must work more?
You'll more easily separate your different logic types between each layer of your application and it'll be easier to make modifications and understand your domain objects and services with this in mind in the long term.
Take for example these two version of the same employee hiring method of a domain service:
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

In the first instance, using a crud repository, you get polluted with translation between different object types representing the same concept (an employee). Only two of the eight lines are pertinent and they're lost in the translation. For the second example, the two same lines doing application logic are the only two lines and it's a lot easier to read this way.

Another clear advantage is when you unit test these methods. The first example will force you to compare the EmployeeBean object your method return with the EmployeeEntity your mocked repository's method returned. If you then add a new field to your EmployeeBean or EmployeeEntity, you might miss this translation, but your test will still pass, probably breaking something down the line. In the second example, if you make your repository return an object and directly compare it to the object returned by the method, there's no way your comparison will fail, and if there are ever changes to your database schemas or domain objects, this code will still function correctly because it doesn't contain the translation.

### Conclusion
Remember that an interface belongs to it's user, not it's implementer. Since the user of the repository is the service, the repository's interface belongs to the service and should talk in terms of domain objects, simplifying the service using it.
