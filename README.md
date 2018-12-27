# Spice <img src="images/spice-logo.png" width="48">

A free and open source software to manage your garden and track plant growth.

## Installation
### Configuring database connection
By default this application targets your MSSQLLocalDB instance. If you don't have one, you can change connection string in appsettings.json file to target any instance of Microsoft SQL Server. Just change the value of `DefaultConnection`. Alternatively, you can add a new key to this dictionary that contains your custom connection string and reference it in Startup.cs:34.

### Creating the database
Spice uses Entity Framework for connecting with database. Entity Framework enables programmers to use code first approach for database strategies and this approach was used to create Spice application. All you have to do is follow these points:
1. If you have a database instance on your SQL Server which is named `SpiceDatabase` then either delete it, pick any other SQL Server instance or change database name in connection string.
2. Make sure that in Solution Explorer default project is set to `Spice.WebAPI`.
3. Open `Packet Manager Console`.
4. Make sure that in Packet Manager Console "Default project" is set to `Spice.Persistence`.
5. Type `update-database` and confirm it by pressing Enter.
6. Database migration succeeded if you see "Done." in the last line of output.

### Launching the application
When database is created, you can launch web application project by pressing F5 in Visual Studio (make sure its still a default project - its name should be displayed **in bold** in Solution Explorer). Now the project will build and when build succeeds a new browser window will be displayed with default page.


## Third Party software used by Spice
Spice relies on the following packages (via NuGet):
* [FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/8.1.2) is used for view model validation purposes.
* [AutoMapper](https://www.nuget.org/packages/AutoMapper/8.0.0) is used to map between entity type and view models.
* [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/2.2.0) is used for database communication.
* [FakeItEasy](https://www.nuget.org/packages/FakeItEasy/4.9.2) is used to setup mock objects in unit tests
* [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/5.5.3) is used to provide fluent api for test assertions
* [NUnit](https://www.nuget.org/packages/NUnit/3.11.0) is used as a test framework
* [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/2.2.0) is used to create a SUT for integration tests

For this software I give absolutely no warranty and I'm not responsible for their behaviour.

## API Overview
### Postman requests set
If you want to test Spice API before you start developing user interface, you can use Postman. All you have to do is import postman-requests.json file containing requests you might want to use. This file is located in the dist folder of this repository. **Please remember that endpoints both described below and saved in postman-request.json might change in future versions.**

### Plants
Spice is all about plants. As of now, the following requests are available:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants - returns list of all plants. Example:
```
[
    {
        "id": "ef9f019b-d93e-4f5b-ba8d-08d66bd675e4",
        "name": "Aji Jobito",
        "species": "Capsicum annuum",
        "state": 2
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/:guid - returns plant details by id (specified guid). Example:
```
// Requested uri: api/plants/ef9f019b-d93e-4f5b-ba8d-08d66bd675e4

{
    "id": "ef9f019b-d93e-4f5b-ba8d-08d66bd675e4",
    "name": "Aji Jobito",
    "species": {
        "id": "d7be6f24-8704-4447-f689-08d66bd60981",
        "name": "Yellow bell pepper",
        "latinName": "Capsicum annuum"
    },
    "field": {
        "id": "2694bd84-fa18-4a35-6a3e-08d66bd634b5",
        "name": "Pole, pole, łyse pole",
        "description": "Tu na razie jest ściernisko, ale będzie San Francisco. A tam, gdzie to kretowisko będzie stał mój bank."
    },
    "row": 1,
    "column": 2,
    "planted": "2018-12-09T15:00:00",
    "state": 2
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/plants - adds new plant with specified data. Example:
```
{
	"Name": "Aji Lemon Drop",
	"SpeciesId": "907083c1-5032-4a28-f688-08d66bd60981",
	"FieldId": "2694bd84-fa18-4a35-6a3e-08d66bd634b5",
	"Row": 0,
	"Column": 0,
	"Planted": "2018-12-09 14:30:00",
	"State": "Healthy"
}
```
Note that if a plant is found growing on specified field, row and column this operation will result in Conflict. The same applies if either species or field does not exist. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/plants/:guid - updates plant data with specified id. Example:
```
// Requested uri: api/plants/ef9f019b-d93e-4f5b-ba8d-08d66bd675e4
{
	"Name": "Aji Jobito",
	"SpeciesId": "d7be6f24-8704-4447-f689-08d66bd60981",
	"FieldId": "2694bd84-fa18-4a35-6a3e-08d66bd634b5",
	"Row": 1,
	"Column": 2,
	"Planted": "2018-12-09 15:00:00",
	"State": "Fruiting"
}
```
Note that if a plant is found growing on specified field, row and column this operation will result in Conflict. The same applies if either species or field does not exist.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/plants/:guid - deletes plant with specified id. Example:
```
// Requested uri: api/plants/ef9f019b-d93e-4f5b-ba8d-08d66bd675e4
// Returns 204 No Content
```
Note that once a plant is deleted it is not possible to restore it!

### Fields
Not all plants grow on fields - some are raised using hydroponics or just in a pot on your balcony. You should rather consider 'Field' as a group of plants, no matter where it grows. As of now, you can make such request to Spice API in fields context:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/fields - returns list of all fields. Example:
```
[
    {
        "id": "10000000-0000-0000-0000-000000000001",
        "name": "Unknown Field",
        "description": "This field was automatically created while applying migration to database. Since it ignores domain restrictions, please move all plants from this field to another one."
    },
    {
        "id": "c4fcb846-65a1-4c86-92fc-08d66b49d1b7",
        "name": "Field, field, endless field",
        "description": "Here's still a wheat stubble, but there will be San Francisco. And over there, where's that molehill there will be my bank."
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/fields/:guid - returns field details by id (specified guid). Example:
```
// Requested uri: api/fields/2694bd84-fa18-4a35-6a3e-08d66bd634b5

{
    "id": "2694bd84-fa18-4a35-6a3e-08d66bd634b5",
    "name": "Field, field, endless field",
    "description": "Here's still a wheat stubble, but there will be San Francisco. And over there, where's that molehill there will be my bank."
    "latitude": 50.9657062,
    "longtitude": 22.3966112,
    "plants": [
        {
            "id": "ef9f019b-d93e-4f5b-ba8d-08d66bd675e4",
            "name": "Aji Jobito",
            "species": "Capsicum annuum",
            "state": 2
        }
    ]
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/plants - adds new field with specified data. Example:
```
{
	"Name": "Field, field, endless field",
	"Description": "Here's still a wheat stubble, but there will be San Francisco. And over there, where's that molehill there will be my bank.",
	"Latitude": 50.9657062,
	"Longtitude": 22.3966112
}
```
Note that if a field with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/fields/:guid - updates field data with specified id. Example:
```
// Requested uri: api/fields/c4fcb846-65a1-4c86-92fc-08d66b49d1b7

{
	"Name": "Pole, pole, łyse pole",
	"Description": "Tu na razie jest ściernisko, ale będzie San Francisco. A tam, gdzie to kretowisko będzie stał mój bank.",
	"Latitude": 50.9657062,
	"Longtitude": 22.3966112
}
```
Note that if a field with specified name already exists this operation will result in Conflict.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/fields/:guid - deletes field with specified id. Example:
```
// Requested uri: api/fields/c4fcb846-65a1-4c86-92fc-08d66b49d1b7
// Returns 204 No Content
```
Note that once a field is deleted it is not possible to restore it and all underlying plants!

### Species
You can also group your plants into species. This might come in handy in the future - you could track which species use higher amount of nutrients, need more sun, or are more prone to pests. For now, you can make such API calls:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/species - returns list of all species. Example:
```
[
    {
        "id": "907083c1-5032-4a28-f688-08d66bd60981",
        "name": "Spicy pepper",
        "latinName": "Capsicum baccatum"
    },
    {
        "id": "d7be6f24-8704-4447-f689-08d66bd60981",
        "name": "Yellow bell pepper",
        "latinName": "Capsicum annuum"
    },
    {
        "id": "b329ee44-084f-48a4-f68a-08d66bd60981",
        "name": "Green bell pepper",
        "latinName": "Capsicum annuum"
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/species/:guid - returns species details by id (specified guid). Example:
```
// Requested uri: api/species/d7be6f24-8704-4447-f689-08d66bd60981

{
    "id": "d7be6f24-8704-4447-f689-08d66bd60981",
    "name": "Yellow bell pepper",
    "latinName": "Capsicum annuum",
    "description": "Not spicy at all. Widely used in hungarian cuisine.",
    "plants": [
        {
            "id": "ef9f019b-d93e-4f5b-ba8d-08d66bd675e4",
            "name": "Aji Jobito",
            "species": "Capsicum annuum",
            "state": 2
        }
    ]
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/species - adds new species with specified data. Example:
```
{
	"Name": "Green bell pepper",
	"LatinName": "Capsicum annuum",
	"Description": "Not spicy at all. Widely used in hungarian cuisine."
}
```
Note that if a species with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/species/:guid - updates species data with specified id. Example:
```
// Requested uri: api/species/d7be6f24-8704-4447-f689-08d66bd60981

{
	"Name": "Spicy pepper",
	"LatinName": "Capsicum baccatum",
	"Description": "To get rid of spice drink a glass of milk or eat a spoon of butter.",
}
```
Note that if a species with specified name already exists this operation will result in Conflict.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/species/:guid - deletes species with specified id. Example:
```
// Requested uri: api/species/d7be6f24-8704-4447-f689-08d66bd60981
// Returns 204 No Content
```
Note that once a species is deleted it is not possible to restore it and all underlying plants!

### Nutrients
Your plants would not survive long without nutrients. With this option you can track the lifecycle of your plants. For now, you can make such API calls:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/nutrients - returns list of all nutrients. Example:
```
[
    {
        "id": "25849c34-3242-4aff-27e3-08d66bfc09eb",
        "name": "Mineral water",
        "description": "Either tap or bottled."
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/nutrients/:guid - returns nutrient details by id (specified guid). Example:
```
// Requested uri: api/nutrients/25849c34-3242-4aff-27e3-08d66bfc09eb

{
    "id": "25849c34-3242-4aff-27e3-08d66bfc09eb",
    "name": "Mineral water",
    "description": "Either tap or bottled.",
    "dosageUnits": "ml"
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/nutrients - adds new nutrient with specified data. Example:
```
{
	"Name": "Mineral water",
	"Description": "Either tap or bottled.",
	"DosageUnits": "ml"
}
```
Note that if a species with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/nutrients/:guid - updates nutrients data with specified id. Example:
```
// Requested uri: api/nutrients/25849c34-3242-4aff-27e3-08d66bfc09eb

{
	"Name": "Fertilizer",
	"Description": "Natural plant fertilizer.",
	"DosageUnits": "g"
}
```
Note that if a species with specified name already exists this operation will result in Conflict.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/nutrients/:guid - deletes nutrients with specified id. Example:
```
// Requested uri: api/nutrients/25849c34-3242-4aff-27e3-08d66bfc09eb
// Returns 204 No Content
```
Note that once a nutrient is deleted it is not possible to restore it and all underlying plants!
