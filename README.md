Telerik.Sitefinity.Samples.IntegrationTests
===========================================

## Integration Tests Sample

The Integration Tests sample project contains integration tests that you can run with the web test runner. 

This project is module-free. 

The sample project provides you with a ready-to-test project that you can use to test the functionality of the web test runner. 

### Requirements

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.


### Installation instructions: SDK Samples from GitHub



1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.
 
The project refers to the following NuGet packages:

**Telerik.Sitefinity.Samples.Common** library

* Telerik.Sitefinity.Core.nupkg

* OpenAccess.Core.nupkg

*  Telerik.Sitefinity.Content.nupkg

**IntegrationTests** library

* Telerik.WebTestRunner.Server.nupkg

* Telerik.Sitefinity.Core.nupkg

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

**SitefinityWebApp** library

* Telerik.Sitefinity.All.nupkg


You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).

### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Run the sample

To run the tests in the Web Test Runner application:

1. Navigate to _Telerik.Sitefinity.Samples.Dependencies_ -> _Sitefinity API Test Runner_ -> _Client_.

    **NOTE**: The **Telerik.Sitefinity.Samples.Dependencies** folder is the one you cloned in the _Installation_ step.
2. Run the **Telerik.WebTestRunner.Client.exe**.

### Additional resources

[Developer's Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Creating integration tests](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/how-to/create-and-run-integration-tests-with-the-Sitefinity-web-test-runner)


