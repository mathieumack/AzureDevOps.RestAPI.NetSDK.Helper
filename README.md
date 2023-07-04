# AzureDevOps.RestAPI.NetSDK.Helper
Helper tools for the Azure DevOps rest api .net SDK

==========

# Onboarding Instructions 

## Installation

1. Add nuget package: 

> Install-Package AzureDevOps.RestAPI.NetSDK.Helper

2. In your application, you must initialize the helper with your Azure DevOps credentials. Open your program.cs or Startup.cs and add the following code:

```csharp
    builder.Services.RegisterVssConnection("https://dev.azure.com/{organization}", "{PAT}");
```

A VssConnection object is now registered in the dependency injection container. You can now inject it in your services or controllers.

```csharp
    public class MyService
    {
        private readonly VssConnection _vssConnection;

        public MyService(VssConnection vssConnection)
        {
            _vssConnection = vssConnection;
        }
    }
```

## Azure DevOps helpers
Here is the list of helpers available in the package:

| Domain | Function | Description | Status |
| ------ | ------ | ------ | ------ |
| Projects | GetProjectList(...) | Get all projects | :white_check_mark: |
| Backlog | GetQueries(...) | Returns all queries available in a project | :white_check_mark: |
| Backlog | GetQueryResults(...) | Execute a query and retrive results | :white_check_mark: |
| Backlog | DownloadAttachment(...) | Download an attachment for a specific work item | :calendar: |
| Wiki | GetWikis() | Retreive all available wikis | :white_check_mark: |
| Wiki | GetWiki(...) | Get a Wiki description by identifier | :white_check_mark: |
| Wiki | GetWikiPages(...) | Retreive all pages for a dedicated Wiki | :white_check_mark: |
| Wiki | GetWikiPageText(...) | Retreive string content for a wiki page | :white_check_mark: |

:white_check_mark: : Available

:calendar: : Planned in the roadmap

You can find more details and samples in the Wiki.

# IC
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mathieumack_AzureDevOps.RestAPI.NetSDK.Helper&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mathieumack_AzureDevOps.RestAPI.NetSDK.Helper)
[![.NET](https://github.com/mathieumack/AzureDevOps.RestAPI.NetSDK.Helper/actions/workflows/ci.yml/badge.svg)](https://github.com/mathieumack/AzureDevOps.RestAPI.NetSDK.Helper/actions/workflows/ci.yml)
[![NuGet package](https://buildstats.info/nuget/AzureDevOps.RestAPI.NetSDK.Helper?includePreReleases=true)](https://nuget.org/packages/AzureDevOps.RestAPI.NetSDK.Helper)

# Documentation : I want more

Do not hesitate to check unit tests on the solution. It's a good way to check how transformations are tested.

Also, to get more samples, go to the [Wiki](https://github.com/mathieumack/AzureDevOps.RestAPI.NetSDK.Helper/wiki). 

Do not hesitate to contribute.


# Support / Contribute
> If you have any questions, problems or suggestions, create an issue or fork the project and create a Pull Request.
