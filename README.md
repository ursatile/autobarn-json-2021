# Autobarn: A Sample .NET Core Application

This is the sample code repository used in Dylan Beattie's workshop "[Introduction to Distributed Systems with .NET Core](https://ursatile.com/workshops/intro-to-distributed-systems-dotnet.html)"

The examples and exercises in the workshop are based on **Autobarn**, a fictional app for selling second-hand cars. This repo contains the code for the starting point of the workshop - an ASP.NET web application and simple data store. During the workshop, we'll create a REST API that car dealers can use to list their stock on our website, we'll create a GraphQL endpoint that can provide data to a single-page web application (SPA) or a mobile app. We'll use a message queueing system to connect our web to several microservices -- a logging service that keeps a record of all new cars listed for sale, and a notification engine that will alert customers when a new car matching their requirements is listed. We'll use gRPC to connect our app to a pricing engine, and finally plug in a SignalR hub so that users who are online can get realtime notifications in their web browser each time a new car is listed for sale.

> Please note that this workshop is an introduction, and the focus is on patterns and principles. The components and services you create during this workshop will have minimal security, monitoring and error recovery; there are links at the end of each section that you can use to find out more about securing, managing and monitoring these kinds of components in a production environment.

## Prerequisites: Getting up and running with ASP.NET Core

The starting point for this workshop is a project called **Autobarn**. It's a very simple ASP.NET Core website that lists second-hand cars available for sale.

If you're going to code along with the exercises in the workshop, you'll need your own copy of the Autobarn project.  I recommend you **fork** this repository to your own GitHub account, and then clone your own fork of the project; that way you can commit and push all the code you'll write during the workshop and refer back to your own work later.

**Autobarn** contains two projects:

* **Autobarn.Website** is an ASP.NET MVC Core website with two controllers and a few views. It has no authentication or user management, and minimal logging.
* **Autobarn.Data** is the data store containing the makes, models and vehicles that are listed for sale on the website. 

Autobarn doesn't need an external database; the source data is all stored in JSON files which are included in the project, so you should be able to clone the repository and run it without requiring any infrastructure or setup other than a .NET runtime.

```
git clone git@github.com:ursatile/autobarn.git
cd Autobarn
dotnet run --project Autobarn.Website
```

You should then be able to browse to https://localhost:4000/ and see the Autobarn homepage.

