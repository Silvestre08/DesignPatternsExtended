# DesignPatternsExtended

A repository is an abstraction that encapsulates data access, making your code testable, reusable as well as maintainable.
The consumer no longer has to know about the underlying data structure. Is it entity framework? A file system, NHibernate?
If we are tightly coupled with a concrete implementation, the application immediately lacks restability.
When we need to use the database somewhere else we would need to duplicate code.
Overall the application lacks testability.

Our application is a .net MVC application. We can see in the initial project that the controller is tightlhy coupled with the data access layer.
It is difficult to write a test for the controller without side effects.

It is also hard to extend entities with domain specific behaviour.

The repository pattern encapsulates the data access layer.
We can replace at runtime with dependency injection/strategy pattern concrete repository implementations.
Now the consumer is decoupled from the data access.
It is easy to write a test without side-effects.
Modify and extend entities befoere they are passed on to the consumer.
It is also a sharable abstraction resulting in less duplication of code.

In our case we introduced a generic repository. A generic repository implements basic functionality that basically all repositories implement.

## Unit of work

The unit of work references multiple repositories. It reduces the communication with the database.
We can add data to the database by calling multiple repositories but we would do it in one transaction.

In our solution each repository has a different instant of EF context. The unit of work guaranties that we work with the same context and commit the transaction in one go.

In large applications we can run into the issue of large unit or work and loose sight of all the repositories that actually belong to it.

## Lazy loading

Lazy loading is a technique that can be used to improve performance of applications. It ensures we loiad data when we only actually need it.

There are different flavors of Lazy loading:

1. Lazy Initialization: the idea behind lazy initialization is the we only proceed to initialize our backing field to an exposed property the first time someone tries access the property. This ideia is implemented in our unit of work class: we only create a repository when we access the property by checking if it is null or not.
   Our example also adds a profile picture to our customer entity. Pictures are expensive and we should only load when they are needed.

Value holders
Virtuak Procies
Ghost Objects
