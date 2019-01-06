# Spice <img src="images/spice-logo.png" width="48">

A free and open source software to manage your garden and track plant growth.

##### Current status
[![Build status](https://ci.appveyor.com/api/projects/status/cyrjed4o78gpjskj/branch/develop?svg=true)](https://ci.appveyor.com/project/JachuPL/spice/branch/develop) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/9dfb48d0323d4a2f9ae5a94fd092cdf5)](https://www.codacy.com/app/JachuPL/Spice?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=JachuPL/Spice&amp;utm_campaign=Badge_Grade)


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

### Running unit tests
Spice comes to you with a wide set of unit and integration tests. To run tests follow these steps:
1. Checkout this repository
2. Restore NuGet packages for solution
   1. If you prefer using command line, execute `dotnet restore`
   2. If you prefer using Visual Studio 2017, open Solution Explorer, right-click the solution item (topmost in the project tree) and click `Restore NuGet packages`
3. Build solution
   1. If you prefer using command line, execute `dotnet build`
   2. If you prefer using Visual Studio 2017, press `Ctrl + Shift + B`
4. Run tests
   1. If you prefer using command line execute `dotnet test` for these projects (paths relative to folder containing .sln file):
      * Application\Tests\Spice.WebAPI.Tests\Spice.WebAPI.Tests.csproj
      * Application\Tests\Spice.Application.Tests\Spice.Application.Tests.csproj
      * Domain\Spice.Domain.Tests\Spice.Domain.Tests.csproj
   2. If you prefer using Visual Studio 2017 open Test Explorer window (Test -> Windows -> Test Explorer), then click `Run all`

### Launching the application
When database is created, you can launch web application project by pressing F5 in Visual Studio (make sure its still a default project - its name should be displayed **in bold** in Solution Explorer). Now the project will build and when build succeeds a new browser window will be displayed with default page.

### Deploying application with Visual Studio 2017 and Web Deploy 3.6
You can deploy Spice to another server or virtual machine is really easy. First, you have to set up remote host. Following these steps will help you configure Windows Server 2016 for web deploy:
1. Install latest Windows Server 2016 either on physical or virtual machine and perform a basic setup.
2. Install IIS Server with all options related (event IIS 6 integration)
3. Download and install [Web Deploy 3.6](https://www.microsoft.com/en-us/download/details.aspx?id=43717) on Windows Server 2016
4. Make sure that access to ports 80 and 8172 is possible through firewall
5. Add `C:\Program Files\IIS\Microsoft Web Deploy V3` to path variable
6. Verify that web deploy service is up and running by vising `https://localhost:8172/msdeploy.axd` in your browser. You should be prompted with authentication box, type in your local account credentials. After confirming the form you will receive 404 error.
7. Download and install [.NET Core SDK 2.2.101](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-windows-x64-installer) and [.NET Core Runtime 2.2.101](https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.0-windows-hosting-bundle-installer)
8. Launch IIS Manager and add new website with basic configuration. Verify that used account has access to chosen path. I used account by credentials.
9. Change settings of application pool used by created site to "No managed code".
10. Install and configure Microsoft SQL Server (Enterprise 2017 in my case) and Microsoft SQL Server Management Studio (17.9.1 in my case)
11. Configure network access to ports related to SQL Server ((here is a full list of ports)[https://docs.microsoft.com/en-us/sql/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access?view=sql-server-2017]))
12. In SQL Server Configuration Manager make sure that value for `Named Pipes` is "Enabled" under SQL Server Network Configuration -> Protocols for MSSQLSERVER
13. Restart SQL Server service
14. Using SQL Server Management Studio create a new login with name and password of your choice.
15. In IIS Manager right-click your website, then Deploy -> Configure Web Deploy Publishing
16. Click the button with three dots located near SQL Server connection string text box
17. Choose SQL Server. In server name type `.`, pick the database name of your choice.
18. Choose the other credentials option and click 'Set' button. Now fill the form with data created in step 13.
19. (*Optional*) You can change the path where your config will be saved using last button with three dots.
20. Confirm form by clicking 'Setup'.
21. Navigate to the folder where your config file was saved and copy it to the machine with Visual Studio 2017 (e.g. via OneDrive, Google Drive or Dropbox).
22. Open Spice solution in Visual Studio 2017, then right-click `Spice.WebAPI` in Solution Explorer and choose `Publish`.
23. Click `Run` button, then `Import Profile` and choose the created publish profile.
24. Click `Configure...` and `Verify connection`. Type in the password for Windows Server's administrator account. Verification passed if a green tick mark is shown on the right.
25. Open publish profile file with text editor and copy value of `SQLServerDBConnectionString`.
26. In the settings tab select `netcoreapp2.2` for target structure, `structure dependent` for deployment mode, and `portable` for runtime. Expand the `Entity Framework platform migrations` and check the checkbox. Paste value copied in previous step to the field below. Click save. **This step might be required each time you're publishing a project to remote server**.
27. Click `Publish` button to start publishing project. You might be prompted to accept untrusted server certificate, do it. You might be prompted again for Windows Server's admin password.
28. After publishing succeed, your project url will open in browser.
29. (*Optional*) After first publishing some files are created in Properties/PublishProfiles. You can add `<AllowUntrustedCertificate>True</AllowUntrustedCertificate>` inside the `Property` group.
Note that there's an example publishing profile in dist folder.

## How do I help?
I'm glad you want to help! Just fork the project, add your changes and create a new pull request. Your pull request will automatically be reviewed by AppVeyor and Codacy. Please remember about unit tests for your changes as all PR's with untested changes will be rejected. And keep your code clean!

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
        "id": "68d7f358-6b2f-444a-61ce-08d6719bd8d4",
        "name": "Avocado #1",
        "species": "Persea americana",
        "state": 0
    },
    {
        "id": "944e487e-0947-485b-61cf-08d6719bd8d4",
        "name": "Avocado #2",
        "species": "Persea americana",
        "state": 0
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid** - returns plant details by id (specified guid). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4

{
    "id": "68d7f358-6b2f-444a-61ce-08d6719bd8d4",
    "name": "Avocado #1",
    "species": {
        "id": "1e7e9575-41e1-470c-9772-08d671982349",
        "name": "Avocado (Hass cultivar)",
        "latinName": "Persea americana"
    },
    "field": {
        "id": "8b6e7f21-98b6-4e46-b2c2-08d67188be3c",
        "name": "Windowsill (bedroom)",
        "description": "Internal windowsill in bedroom."
    },
    "row": 0,
    "column": 0,
    "planted": "2018-11-01T09:30:00",
    "state": 0,
    "nutrients": [
        {
            "id": "b57da65c-f0d8-4566-3ad8-08d6719f52ff",
            "name": "Mineral water",
            "amount": "100 ml",
            "date": "2018-11-03T14:30:00"
        },
        {
            "id": "36e719ea-9020-4682-3ad9-08d6719f52ff",
            "name": "Mineral water",
            "amount": "150 ml",
            "date": "2018-11-08T08:30:00"
        },
        {
            "id": "5a7ae091-c21f-48aa-3ada-08d6719f52ff",
            "name": "Mineral water",
            "amount": "50 ml",
            "date": "2018-11-10T08:00:00"
        },
        {
            "id": "78a4bbd5-28fa-4a8c-3adb-08d6719f52ff",
            "name": "Mineral water",
            "amount": "250 ml",
            "date": "2018-11-20T19:40:00"
        },
        {
            "id": "cff14009-206f-4e15-3adc-08d6719f52ff",
            "name": "Organic fertilizer",
            "amount": "50 g",
            "date": "2018-11-09T11:25:00"
        },
        {
            "id": "f4696a14-6b2b-43db-3add-08d6719f52ff",
            "name": "Organic fertilizer",
            "amount": "150 g",
            "date": "2018-12-01T17:31:00"
        },
        {
            "id": "094f9561-9f43-44c1-3ade-08d6719f52ff",
            "name": "Anti-insect and Anti-fungi sticks",
            "amount": "2 pcs",
            "date": "2018-12-11T11:47:00"
        }
    ],
    "events": [
        {
            "id": "a1669fdb-a471-45b3-3d3e-08d6719bd8da",
            "type": 8,
            "occured": "2019-01-03T17:54:05.9362537"
        },
        {
            "id": "8d981e89-eeae-4e9c-3d3f-08d6719bd8da",
            "type": 6,
            "occured": "2019-01-03T17:57:05.759464"
        },
        {
            "id": "53525e74-0eb3-4538-3d40-08d6719bd8da",
            "type": 6,
            "occured": "2019-01-03T17:57:12.1350025"
        },
        {
            "id": "4e3c64df-a412-462a-3d42-08d6719bd8da",
            "type": 7,
            "occured": "2018-12-30T13:05:15"
        },
        {
            "id": "a2d7701f-72ff-40f5-3d43-08d6719bd8da",
            "type": 9,
            "occured": "2018-11-03T14:30:00"
        },
        {
            "id": "fb0080ee-4310-4e0b-3d44-08d6719bd8da",
            "type": 9,
            "occured": "2018-11-08T08:30:00"
        },
        {
            "id": "dccf0322-0832-4f3e-3d45-08d6719bd8da",
            "type": 9,
            "occured": "2018-11-10T08:00:00"
        },
        {
            "id": "d0ef6ffd-3118-4501-3d46-08d6719bd8da",
            "type": 9,
            "occured": "2018-11-20T19:40:00"
        },
        {
            "id": "f098f148-bd34-4c96-3d47-08d6719bd8da",
            "type": 9,
            "occured": "2018-11-09T11:25:00"
        },
        {
            "id": "494297d3-2338-4424-3d48-08d6719bd8da",
            "type": 9,
            "occured": "2018-12-01T17:31:00"
        },
        {
            "id": "437efb82-e8b1-4a25-3d49-08d6719bd8da",
            "type": 9,
            "occured": "2018-12-11T11:47:00"
        },
        {
            "id": "7bf15af3-83fa-4d4f-3d4a-08d6719bd8da",
            "type": 9,
            "occured": "2018-12-11T11:47:00"
        }
    ]
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/plants - adds new plant with specified data. Example:
```
{
	"Name": "Avocado #1",
	"SpeciesId": "1e7e9575-41e1-470c-9772-08d671982349",
	"FieldId": "8b6e7f21-98b6-4e46-b2c2-08d67188be3c",
	"Row": 0,
	"Column": 0,
	"Planted": "2018-11-01 09:30:00",
	"State": "Healthy"
}
```
Note that if a plant is found growing on specified field, row and column this operation will result in Conflict. The same applies if either species or field does not exist. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/plants/**guid** - updates plant data with specified id. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4

{
	"Name": "Avocado #2",
	"SpeciesId": "73531e76-eaa7-42e8-9773-08d671982349",
	"FieldId": "b1ed0592-5e91-4690-b2c3-08d67188be3c",
	"Row": 1,
	"Column": 0,
	"Planted": "2018-11-02 10:00:00",
	"State": "Fruiting"
}
```
Note that if a plant is found growing on specified field, row and column this operation will result in Conflict. The same applies if either species or field does not exist.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/plants/**guid** - deletes plant with specified id. Example:
```
// Requested uri: api/plants/944e487e-0947-485b-61cf-08d6719bd8d4
// Returns 204 No Content
```
Note that once a plant is deleted it is not possible to restore it!

### Fields
Not all plants grow on fields - some are raised using hydroponics or just in a pot on your balcony. You should rather consider 'Field' as a group of plants, no matter where it grows. As of now, you can make such request to Spice API in fields context:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/fields - returns list of all fields. Example:
```
[
    {
        "id": "8b6e7f21-98b6-4e46-b2c2-08d67188be3c",
        "name": "Windowsill (bedroom)",
        "description": "Internal windowsill in bedroom."
    },
    {
        "id": "b1ed0592-5e91-4690-b2c3-08d67188be3c",
        "name": "Windowsill (living room)",
        "description": "Internal windowsill in living room."
    },
    {
        "id": "9e444078-0898-44f6-b2c4-08d67188be3c",
        "name": "Balcony",
        "description": "Balcony in living room."
    },
    {
        "id": "ba9d4125-ca60-49f4-b2c5-08d67188be3c",
        "name": "Field",
        "description": "Lots of sun from early morning untill afternoon."
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/fields/**guid** - returns field details by id (specified guid). Example:
```
// Requested uri: api/fields/8b6e7f21-98b6-4e46-b2c2-08d67188be3c

{
    "id": "8b6e7f21-98b6-4e46-b2c2-08d67188be3c",
    "name": "Windowsill (bedroom)",
    "description": "Internal windowsill in bedroom.",
    "latitude": 50.9657062,
    "longtitude": 22.3966112,
    "plants": [
        {
            "id": "68d7f358-6b2f-444a-61ce-08d6719bd8d4",
            "name": "Avocado #1",
            "species": "Persea americana",
            "state": 0
        }
    ]
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/fields - adds new field with specified data. Example:
```
{
	"Name": "Field",
	"Description": "Lots of sun from early morning untill afternoon.",
	"Latitude": 50.9657062,
	"Longtitude": 22.3966112
}
```
Note that if a field with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/fields/**guid** - updates field data with specified id. Example:
```
// Requested uri: api/fields/ba9d4125-ca60-49f4-b2c5-08d67188be3c

{
	"Name": "Field B",
	"Description": "Sunny from the afternoon until the sunset.",
	"Latitude": 50.9657062,
	"Longtitude": 22.3966112
}
```
Note that if a field with specified name already exists this operation will result in Conflict.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/fields/**guid** - deletes field with specified id. Example:
```
// Requested uri: api/fields/ba9d4125-ca60-49f4-b2c5-08d67188be3c
// Returns 204 No Content
```
Note that once a field is deleted it is not possible to restore it and all underlying plants!

### Species
You can also group your plants into species. This might come in handy in the future - you could track which species use higher amount of nutrients, need more sun, or are more prone to pests. For now, you can make such API calls:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/species - returns list of all species. Example:
```
[
    {
        "id": "1e7e9575-41e1-470c-9772-08d671982349",
        "name": "Avocado (Hass cultivar)",
        "latinName": "Persea americana"
    },
    {
        "id": "73531e76-eaa7-42e8-9773-08d671982349",
        "name": "Avocado (Fuerte cultivar)",
        "latinName": "Persea americana"
    },
    {
        "id": "ef0ab7dc-aed5-40cc-9774-08d671982349",
        "name": "Bell pepper",
        "latinName": "Capsicum annuum"
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/species/**guid** - returns species details by id (specified guid). Example:
```
// Requested uri: api/species/1e7e9575-41e1-470c-9772-08d671982349

{
    "id": "1e7e9575-41e1-470c-9772-08d671982349",
    "name": "Avocado (Hass cultivar)",
    "latinName": "Persea americana",
    "description": "Know by Latin American cultures as 'butter of the gods'.",
    "plants": [
        {
            "id": "68d7f358-6b2f-444a-61ce-08d6719bd8d4",
            "name": "Avocado #1",
            "species": "Persea americana",
            "state": 0
        }
    ]
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/species - adds new species with specified data. Example:
```
{
	"Name": "Bell pepper",
	"LatinName": "Capsicum annuum",
	"Description": "Sweet pepper with no spiciness at all."
}
```
Note that if a species with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/species/**guid** - updates species data with specified id. Example:
```
// Requested uri: api/species/ef0ab7dc-aed5-40cc-9774-08d671982349

{
	"Name": "Spicy pepper",
	"LatinName": "Capsicum baccatum",
	"Description": "To get rid of spice drink a glass of milk or eat a spoon of butter.",
}
```
Note that if a species with specified name already exists this operation will result in Conflict.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/species/**guid** - deletes species with specified id. Example:
```
// Requested uri: api/species/ef0ab7dc-aed5-40cc-9774-08d671982349
// Returns 204 No Content
```
Note that once a species is deleted it is not possible to restore it and all underlying plants!

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/species/**guid**/summary - returns summary of nutrients applied to all plants of selected species. Example:
```
// Requested uri: api/species/1e7e9575-41e1-470c-9772-08d671982349/summary
// Note that you can get nutrition summary for selected period of time by adding start and end date parameters, eg: api/species/1e7e9575-41e1-470c-9772-08d671982349/summary?fromDate=2018-01-01T00:00:00&toDate=2018-12-31T23:59:59

[
    {
        "nutrient": {
            "id": "e95b4df9-cec7-439d-f47d-08d6719b259d",
            "name": "Mineral water",
            "description": "Either tap or bottled.",
            "dosageUnits": "ml"
        },
        "totalAmount": 550,
        "firstAdministration": "2018-11-03T14:30:00",
        "lastAdministration": "2018-11-20T19:40:00"
    },
    {
        "nutrient": {
            "id": "72a26221-45f4-43dd-f47e-08d6719b259d",
            "name": "Organic fertilizer",
            "description": "Some plants do not require it.",
            "dosageUnits": "g"
        },
        "totalAmount": 200,
        "firstAdministration": "2018-11-09T11:25:00",
        "lastAdministration": "2018-12-01T17:31:00"
    },
    {
        "nutrient": {
            "id": "66f3ec7e-d702-427b-f47f-08d6719b259d",
            "name": "Anti-insect and Anti-fungi sticks",
            "description": "Just put two into the soil.",
            "dosageUnits": "pcs"
        },
        "totalAmount": 2,
        "firstAdministration": "2018-12-11T11:47:00",
        "lastAdministration": "2018-12-11T11:47:00"
    }
]
```

### Nutrients
Nutrients are very important in your plants growth. You can add some for further progress tracking using these endpoints:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/nutrients - returns list of all nutrients. Example:
```
[
    {
        "id": "e95b4df9-cec7-439d-f47d-08d6719b259d",
        "name": "Mineral water",
        "description": "Either tap or bottled."
    },
    {
        "id": "72a26221-45f4-43dd-f47e-08d6719b259d",
        "name": "Organic fertilizer",
        "description": "Some plants do not require it."
    },
    {
        "id": "66f3ec7e-d702-427b-f47f-08d6719b259d",
        "name": "Anti-insect and Anti-fungi sticks",
        "description": "Just put two into the soil."
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/nutrients/**guid** - returns nutrient details by id (specified guid). Example:
```
// Requested uri: api/nutrients/e95b4df9-cec7-439d-f47d-08d6719b259d

{
    "id": "e95b4df9-cec7-439d-f47d-08d6719b259d",
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
Note that if a nutrient with specified name already exists this operation will result in Conflict. Please keep in mind that response contains 'Location' header with URI to newly created resource.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/nutrients/**guid** - updates nutrients data with specified id. Example:
```
// Requested uri: api/nutrients/72a26221-45f4-43dd-f47e-08d6719b259d

{
	"Name": "Fertilizer",
	"Description": "Natural plant fertilizer.",
	"DosageUnits": "g"
}
```
Note that if a nutrient with specified name already exists this operation will result in Conflict. **The same thing applies if a nutrient was already administered to any plant, thus you can only edit unadministered nutrients.**

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/nutrients/**guid** - deletes nutrients with specified id. Example:
```
// Requested uri: api/nutrients/72a26221-45f4-43dd-f47e-08d6719b259d
// Returns 204 No Content
```
Note that once a nutrient is deleted it is not possible to restore it and all underlying plants!

### Plant nutrients
Your plants would not survive long without nutrients. With this option you can track the lifecycle of your plants. For now, you can make such API calls:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/nutrients - returns list of administered plant nutrients. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients

[
    {
        "id": "b57da65c-f0d8-4566-3ad8-08d6719f52ff",
        "name": "Mineral water",
        "amount": "100 ml",
        "date": "2018-11-03T14:30:00"
    },
    {
        "id": "36e719ea-9020-4682-3ad9-08d6719f52ff",
        "name": "Mineral water",
        "amount": "150 ml",
        "date": "2018-11-08T08:30:00"
    },
    {
        "id": "5a7ae091-c21f-48aa-3ada-08d6719f52ff",
        "name": "Mineral water",
        "amount": "50 ml",
        "date": "2018-11-10T08:00:00"
    },
    {
        "id": "78a4bbd5-28fa-4a8c-3adb-08d6719f52ff",
        "name": "Mineral water",
        "amount": "250 ml",
        "date": "2018-11-20T19:40:00"
    },
    {
        "id": "cff14009-206f-4e15-3adc-08d6719f52ff",
        "name": "Organic fertilizer",
        "amount": "50 g",
        "date": "2018-11-09T11:25:00"
    },
    {
        "id": "f4696a14-6b2b-43db-3add-08d6719f52ff",
        "name": "Organic fertilizer",
        "amount": "150 g",
        "date": "2018-12-01T17:31:00"
    },
    {
        "id": "094f9561-9f43-44c1-3ade-08d6719f52ff",
        "name": "Anti-insect and Anti-fungi sticks",
        "amount": "2 pcs",
        "date": "2018-12-11T11:47:00"
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/nutrients/**guid** - returns administered plant nutrient details by plant id (first guid parameter) and nutrition record id (second guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients/094f9561-9f43-44c1-3ade-08d6719f52ff

{
    "id": "094f9561-9f43-44c1-3ade-08d6719f52ff",
    "nutrient": {
        "id": "66f3ec7e-d702-427b-f47f-08d6719b259d",
        "name": "Anti-insect and Anti-fungi sticks",
        "description": "Just put two into the soil.",
        "dosageUnits": "pcs"
    },
    "amount": 2,
    "date": "2018-12-11T11:47:00"
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/plants/**guid**/nutrients - adds new nutrition record with specified data. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients

{
	"NutrientId": "66f3ec7e-d702-427b-f47f-08d6719b259d",
	"Amount": 2.0,
	"Date": "2018-12-11 11:47:00",
	"CreateEvent": true
}
```
Note that if a plant is not found this operation will result in Conflict. The same applies if nutrient does not exist. Also, you will receive a Conflict response if specified date is earlier than plant date. Please keep in mind that response contains 'Location' header with URI to newly created resource. The date parameter is completely optional - a request processing date is used if not specified otherwise.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/plants/**guid**/nutrients/**guid** - updates nutrition record with specified id (second guid parameter) for plant with specified id (first guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients/094f9561-9f43-44c1-3ade-08d6719f52ff

{
	"NutrientId": "66f3ec7e-d702-427b-f47f-08d6719b259d",
	"Amount": 3.0,
	"Date": "2018-12-11 11:47:00",
	"CreateEvent": true
}
```
Note that if a plant is not found this operation will result in Conflict. The same applies if either nutrient or the nutrition record itself does not exist. Also, you will receive a Conflict response if specified date is earlier than plant date.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/plants/**guid**/nutrients/**guid** - deletes nutrition record with specified id (second guid parameter) from plant with specified id (first guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients/094f9561-9f43-44c1-3ade-08d6719f52ff
// Returns 204 No Content
```
Note that once a nutrition record is deleted it is not possible to restore it! **Important**: if an event was created while adding nutrition info it won't be deleted and you have to remove such event manually!

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/nutrients/summary - returns summary of nutrients administered to a plant grouped by nutrient. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients/summary
// Note that you can get nutrition info for selected period of time by adding start and end date parameters, eg: plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/nutrients/summary?fromDate=2018-01-01T00:00:00&toDate=2018-12-31T23:59:59

[
    {
        "nutrient": {
            "id": "e95b4df9-cec7-439d-f47d-08d6719b259d",
            "name": "Mineral water",
            "description": "Either tap or bottled.",
            "dosageUnits": "ml"
        },
        "totalAmount": 550,
        "firstAdministration": "2018-11-03T14:30:00",
        "lastAdministration": "2018-11-20T19:40:00"
    },
    {
        "nutrient": {
            "id": "72a26221-45f4-43dd-f47e-08d6719b259d",
            "name": "Organic fertilizer",
            "description": "Some plants do not require it.",
            "dosageUnits": "g"
        },
        "totalAmount": 200,
        "firstAdministration": "2018-11-09T11:25:00",
        "lastAdministration": "2018-12-01T17:31:00"
    },
    {
        "nutrient": {
            "id": "66f3ec7e-d702-427b-f47f-08d6719b259d",
            "name": "Anti-insect and Anti-fungi sticks",
            "description": "Just put two into the soil.",
            "dosageUnits": "pcs"
        },
        "totalAmount": 2,
        "firstAdministration": "2018-12-11T11:47:00",
        "lastAdministration": "2018-12-11T11:47:00"
    }
]
```

### Plant events
There's a lot happening during your plant lifecycle. With this option you can track all the events that occur. For now, you can make such API calls:
* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/events - returns list of plant events. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events

[
    {
        "id": "a1669fdb-a471-45b3-3d3e-08d6719bd8da",
        "type": 8,
        "occured": "2019-01-03T17:54:05.9362537"
    },
    {
        "id": "8d981e89-eeae-4e9c-3d3f-08d6719bd8da",
        "type": 6,
        "occured": "2019-01-03T17:57:05.759464"
    },
    {
        "id": "53525e74-0eb3-4538-3d40-08d6719bd8da",
        "type": 6,
        "occured": "2019-01-03T17:57:12.1350025"
    }
]
```

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/events/**guid** - returns plant event details by plant id (first guid parameter) and event record id (second guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events/4e3c64df-a412-462a-3d42-08d6719bd8da

{
    "id": "4e3c64df-a412-462a-3d42-08d6719bd8da",
    "type": 7,
    "description": "Great progress after fertilizing.",
    "occured": "2018-12-30T13:05:15"
}
```

* ![POST Request](https://img.shields.io/badge/Method-POST-yellow.svg) api/plants/**guid**/events - adds new event with specified data. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events

{
	"Type": "Insects",
	"Description": "Spotted some Leptinotarsa decemlineata on the leaves today.",
    "Occured": "2018-12-30 13:00:00"
}
```
Note that if a plant is not found this operation will result in Conflict. Also, you will receive a Conflict response if specified occurence date is earlier than plant date or in the future. Please keep in mind that response contains 'Location' header with URI to newly created resource. The occurence date parameter is completely optional - a request processing date is used if not specified otherwise. The other optional parameter is 'CreateEvent' - if it is not specified, default value is false.

* ![PUT Request](https://img.shields.io/badge/Method-PUT-blue.svg) api/plants/**guid**/events/**guid** - updates event record with specified id (second guid parameter) for plant with specified id (first guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events/4e3c64df-a412-462a-3d42-08d6719bd8da

{
    "Type": "Growth",
    "Description": "Great progress after fertilizing.",
    "Occured": "2018-12-30T13:05:15"
}
```
Note that if a plant is not found this operation will result in Conflict. The same applies if event record does not exist. Also, you will receive a Conflict response if specified occurence date is earlier than plant date or in the future.

* ![DELETE Request](https://img.shields.io/badge/Method-DELETE-red.svg) api/plants/**guid**/events/**guid** - deletes event record with specified id (second guid parameter) from plant with specified id (first guid parameter). Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events/4e3c64df-a412-462a-3d42-08d6719bd8da
// Returns 204 No Content
```
Note that once an event record is deleted it is not possible to restore it!

* ![GET Request](https://img.shields.io/badge/Method-GET-brightgreen.svg) api/plants/**guid**/events/summary - returns summary of events occured to a plant grouped by event type. Example:
```
// Requested uri: api/plants/68d7f358-6b2f-444a-61ce-08d6719bd8d4/events/summary
// Note that you can get event log for selected period of time by adding start and end date parameters, eg: plants/ef9f019b-d93e-4f5b-ba8d-08d66bd675e4/events/summary?fromDate=2018-01-01T00:00:00&toDate=2018-12-31T23:59:59

[
    {
        "type": 6,
        "totalCount": 2,
        "firstOccurence": "2019-01-03T17:57:05.759464",
        "lastOccurence": "2019-01-03T17:57:12.1350025"
    },
    {
        "type": 7,
        "totalCount": 1,
        "firstOccurence": "2018-12-30T13:05:15",
        "lastOccurence": "2018-12-30T13:05:15"
    },
    {
        "type": 8,
        "totalCount": 1,
        "firstOccurence": "2019-01-03T17:54:05.9362537",
        "lastOccurence": "2019-01-03T17:54:05.9362537"
    }
]
```