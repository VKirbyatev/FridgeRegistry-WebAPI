<h1 align="center">FridgeRegistry</h1>

<h4 align="center">A project to keep track of the food in your fridge</h4>



## Description
---

A simple **REST API** project to keep track of food in the fridge. Provides small functionality to make a tree of categories and add products to it. Then you can put products from the catalogue into your storehouse with comments and production date. The program will calculate the shelf life of the product and you don't have to keep everything in your head. 

## Usage
---

Application has two ASP.NET web projects. First one contains core application business logic (```WebAPI```), and the second one contains authentication and authorization logic (```Identity```).

You can see their documentation by ```/swagger``` endpoint.

All endpoints that return collections have pagination and a search string. The user's products can also be sorted by name or expiration date.

## Getting Started
---

Firstly you should register or log-in as administrator via identity service (running at ```http//:3000``` or ```https//:3001``` ports by default)

Administrator account creates by default if app running in development mode, and there is no records in user table.

Default administrator account credentials is:

```json
{
  "login": "admin@mail.ru",
  "password": "AdminPassword123!"
}
```

Then as administrator you should create product's categories (core application running at ```http//:4000``` or ```https//:4001``` ports by default). They has **tree structure**, so all categories can have single parent and many children.

After that add some products to categories. All products should have a **Name, Description** and **ShelfLife**.

Congratulations! Now any type of authorized users can put products from categories to their own storage. The program will calculate products expiry date based on the creation date entered by the user and the expiration date specified by the administrator.

Products added by the user to his own storage will be visible and available for modification only to himself.

## Running in Docker

1. Download and install docker from [official site](https://docs.docker.com/get-docker/).
2. Add development ssl certificates for https using this [guide](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0#running-pre-built-container-images-with-https)
3. Then open docker compose file and edit these lines:
   
```yml
# Located int web_api and identity at environment section

## Replace fridge-registry to your dev-certs password
- ASPNETCORE_Kestrel__Certificates__Default__Password=fridge-registry

## Replace /https/aspnetapp.pfx to %USERPROFILE%\.aspnet\https:/https/ if your are using windows platform
- ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
```

4. Start containers with this command:
```shell
docker-compose up --build -d
```

## Running via .NET CLI
1. Download [.NET sdk](https://docs.microsoft.com/en-us/dotnet/core/sdk)
2. Configure ```appsettings.Development.json``` files in FridgeRegistry.WebAPI and FridgeRegistry.Identity directories.
3. Update the connection string according to your installed database
4. Run ```dotnet run``` command in FridgeRegistry.WebAPI/FridgeRegistry.Identity directories.

***Warnings***
If you configured JWT secret, make sure that it is the same at WebAPI and Identity applications.
   
