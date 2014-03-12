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


### Installation instructions: SDK Samples from GitHub

1. Clone the [Telerik.Sitefinity.Samples.Dependencies](https://github.com/Sitefinty-SDK/Telerik.Sitefinity.Samples.Dependencies) repo to get all assemblies neccessary to run for the samples.
2. Fix broken references in the class libraries, for example in **SitefinityWebApp** and **Telerik.Sitefinity.Samples.Common**:

  1. In Solution Explorer, open the context menu of your project node and click _Properties_.  
  
    The Project designer is displayed.
  2. Select the _Reference Paths_ tab page.
  3. Browse and select the folder where **Telerik.Sitefinity.Samples.Dependencies** folder is located.
  4. Click the _Add Folder_ button.


3. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
4. Modify the **connectionString** value to match your server address.

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


