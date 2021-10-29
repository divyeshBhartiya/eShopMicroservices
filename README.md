# eShopMicroservices

## Big Picture

![eShopMicroservices](https://user-images.githubusercontent.com/16538471/139399254-21e931cf-04c1-4908-9641-eb1f46a63bee.png)


Building Microservices on .Net platforms which used Asp.Net Web API, Docker, RabbitMQ, MassTransit, Grpc, Ocelot API Gateway, MongoDB, Redis, PostgreSQL, SqlServer, Dapper, Entity Framework Core, CQRS and Clean Architecture implementation.

Developing e-commerce modules over Product, Basket and Ordering microservices with NoSQL (MongoDB, Redis) and Relational databases (PostgreSQL, Sql Server) with communicating over RabbitMQ Event Driven Communication and Ocelot API Gateway which could be extended for implementing reverse proxy or similar.

The idea is, when an user would create/update Basket, (which gets saved in Redis), a Discount gRPC service would be called and update the total price of the basket (Discount rates are saved in DiscountDb, PostgreSQL). When user checkout the basket, a Basket Checkout Event would be fired and gets queued in the RabbitMQ. This event is handled at Order Microservice. This would get save at the OrderDb (SQL Server). Once the checkout event is fired to the RabbitMQ, the Basket Microservice deletes the Basket from the BasketDb(Redis). Once the Order is created in the OrderDb, you can query the order using GetOrders endpoint.

There is a Catalog Microservice as well, which handles the product records and uses MongoDb for persistence.
The complete setup has docker support with DockerFiles for individual Microservices and respective DBs. This setup is orchaestrated by docker-compose.

## Contains:
ASPNET Core Web API Development of Microservices.

REST API Principles, CRUD Operations.

Mongo DB and Redis NoSQL Database Connection on Docker.

Entity Framework Core with SQL Server Database Connection on Docker.

N-Layer implementation with Repository Pattern.

Swagger Open API implementation.

Consume Discount Grpc Service for inter-service sync communication to calculate product final price.

Publish BasketCheckout Queue with using MassTransit and RabbitMQ.

Build a Highly Performant inter-service gRPC Communication with Basket Microservice.

Using Dapper for micro-orm implementation to simplify data access and ensure high performance.

PostgreSQL database connection and containerization.

Async Microservices Communication with RabbitMQ Message-Broker Service.

Using RabbitMQ Publish/Subscribe Topic Exchange Model.

Using MassTransit for abstraction over RabbitMQ Message-Broker system.

Implementing DDD, CQRS, and Clean Architecture with using Best Practices.

Developing CQRS with using MediatR, FluentValidation and AutoMapper packages.

Consuming RabbitMQ BasketCheckout event queue with using MassTransit-RabbitMQ Configuration.

Using Entity Framework Core ORM and auto migrate to SqlServer when application startup.

Ocelot API Gateway Development of Microservices.

Call Ocelot APIs with HttpClientFactory.

The Gateway aggregation pattern in Shopping Aggregator.

ASPNET Core Web Application with Bootstrap 4 and Razor template.

Docker Compose Containerization of All Microservices.

pgAdmin PostgreSQL Tools feature rich Open Source administration and development platform for PostgreSQL.

## Courtesy: 
https://github.com/mehmetozkaya

## References:
https://medium.com/aspnetrun/cqrs-and-event-sourcing-in-event-driven-architecture-of-ordering-microservices-fb67dc44da7a
https://www.udemy.com/
