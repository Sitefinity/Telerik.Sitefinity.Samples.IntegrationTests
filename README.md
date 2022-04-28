Progress.Sitefinity.Samples.IntegrationTests
===========================================

## Integration Tests Sample

The Integration Tests sample project contains integration tests that you can run with the web test runner. 

This project is module-free. 

The sample project provides you with a ready-to-test project that you can use to test the functionality of the web test runner. 

### Requirements

* Sitefinity CMS license
* .NET Framework 4.8
* Visual Studio 2019
* Microsoft SQL Server 2012 or later versions

### Nuget package restoration
The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
For a full list of the referenced packages and their versions see the [packages.config](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.IntegrationTests/blob/master/SitefinityWebApp/packages.config) file.    
For a history and additional information related to package versions on different releases of this repository, see the [Releases page](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.IntegrationTests/releases).    

### Installation instructions:

1. Build the solution
2. Host the SitefinityWebApp in IIS or launch directly in IIS Express from Visual Studio
3. Follow the SetUp process instructions
4. Set LogIn credentials
5. Locate your license
 

### Run the sample
There are 2 ways to execute the tests:
* UI Client approach - provides application with UI to execute the tests.
1. Navigate to *Telerik.WebTestRunner.Client* folder.
2. Run the **Telerik.WebTestRunner.Client.exe**.
3. Enter the url to your website
4. Enter the credentials to your website
5. The sample tests should appear
6. From the bottom left corner you can execute all tests, or run them one by one.

* ClI - non-interactive approach - [detailed information](https://github.com/Sitefinity/Telerik.Sitefinity.Samples.IntegrationTests/edit/master/README.md#using-cmd-integration-tests-runner): 
1. Navigate to *Telerik.WebTestRunner.Client* folder.
2. Run the **Telerik.WebTestRunner.Cmd.exe**.
The CLI works with the following parameters:



### General guidelines on implementing integrated tests
* Tests need to be atomic. Do not rely on the order in which tests will be run.
* Do not expect anything from Sitefinity CMS except the initial state with which it has been installed.
* Always have in mind what do you want to test. Do not make tests just to verify that “nothing breaks”

### How to write integration tests for Sitefinity CMS?
All Integration tests should be places in assemblies that contains integration tests attribute.

1. Create a new class in your Integration Tests project.
2. Place the ```[TestFixture]``` attribute on top of the class, so that the framework recognizes the class as test fixture.
3. Place the ```[Description("...")]``` attribute on top of the class and briefly describe the test fixture.
4. Implement a public void method that reporesents your test.
5. Put a ```[Test]``` attribute on top of the method that represents your test.
6. Put a ```[Description("...")]``` attribute on top of the method and briefly describe the test purpose.
7. Implement the test. The Implementation is identical to that of an unit test (except that you have everything at your disposal as you would in a Sitefinity CMS page)
8. You can use ```[FixtureSetUp]``` and ```[FixtureTearDown]``` attribute to mark methods to be executed at the beginning of a fixture test case execution and at the end. Both methods are optional and are allowed only once per class.
If ```[FixtureSetUp]``` invocation fails, no test case are executed and they are all marked as failures.
If ```[FixtureTearDown]``` invocation fails again all tests are marked as failures.
If ```[SetUp]``` of a test fails, the method-body and the ```[TearDown]``` won't be executed, so you should clean all data that might left after your tests using ```[FixtureTearDown]```. Internal test methods might not be interpreted correctly by the reflection invocation.

Example:
```c#
using MbUnit.Framework;
using System.Linq;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;

namespace MyNamspace
{

     [TestFixture]
     [Description("Integration tests for the ecommerce catalog products.")]
     public class Products
     {
         [Test]
         [Description("Tests that a product can be created through classic API.")]
         public void CreateProduct()
         {
            string productTitle = "Product 1";
            string productDescription = "Product description 1";
            double weight = 10;
            double price = 25;
            string sku = "Product SKU 1"; 
            
            var catalogManager = CatalogManager.GetManager();
            var product = catalogManager.CreateProduct();
            product.Title = productTitle;
            product.Description = productDescription;
            product.Weight = weight;
            product.Price = price;
            product.Sku = sku;

            catalogManager.SaveChanges();

            var existingProduct = catalogManager.GetProducts()
                                               .Where(p => p.Title == productTitle)
                                               .SingleOrDefault();

            Assert.AreEqual(productTitle, existingProduct.Title.Value);
            Assert.AreEqual(productDescription, existingProduct.Description.Value);
            Assert.AreEqual(weight, existingProduct.Weight);
            Assert.AreEqual(price, existingProduct.Price);
            Assert.AreEqual(sku, existingProduct.Sku);

            // clean up

            catalogManager.DeleteProduct(existingProduct);
            catalogManager.SaveChanges();
         }
      }
}
```

### Multilingual Support
In order to execute a test in multilingual mode you need to add [Multilingual] attribute to the test.
You can pass one of the following modes in the constructor:
* Multilingual - This specifies that this test will be executed once the web site is in multilingual mode.
* MultiAndMonoLingual - this specifies that the test will be executed first in monolingual and then i multilingual mode of the website.
* Upgrade - This specifies that this test will be executed in monolingual mode, right before the web site is configured to multilingual. Then it will be executed once again after the transition to multilingual. This enables your tests to be non-atomic - the first time after it is executed it leaves some data for the second test to check    
(test what happens to a given data after the upgrade) so you can work with the data after setting the localization of the website.

**You should not hardcode the culture in a test - Example: ```C# var en = new CultureInfo("en-US"); ```**

### SSL Support
In order to execute a tests on SSL you need to use the SSL category filter attribute on the test method.
```C#   [Category(TestCategories.Ssl)] ```

Example:
```C#
 [Test]
 [Category(TestCategories.Ssl)]
 [Author(TestAuthor.Team1)]
 [Description("Ssl test")]
 public void Test1()
 {...}
```

### Different User execution support
In order to execute a test with a different user you need to use the Authenticate Attribute on the test method.
```C# [AuthenticateAttribute(username, password, "provider")] ```

Note: The _AuthenticateAttribute_ accepts a provider parameter which you can pass in order to authenticate with a speciffic membership provider such as a _LDAP_ provider. If you skip this parameter the default membership provider will be used.

Example:
```C#
 [Test]
 [Category(TestCategories.Ssl)]
 [Author(TestAuthor.Team1)]
 [AuthenticateAttribute("editor", "password", "provider")]
 [Description("Ssl test")]
 public void Test1()
 {...}
```

**In order to run the test with different user you have to create it first in the [FixtureSetUp] or in the [SetUp]**
You can use the following code to add the user without being logged in.
**Option 1:**
```C#
UserManager userManager = UserManager.GetManager("Default");
using (new ElevatedModeRegion(userManager ))
{....}
```
**Option 2:**
```C#
 userManager.Provider.SupressSecurityCheck = true;
```

**Note:**
[FixtureSetUp] and [FixtureTearDown] are executed with the user that is set in the app.config or Site setting for the client application.
[SetUp] and [TearDown] are executed with the user you have set in the AuthenticateAttribute. If it is not set, then they are executed with the same user set in the app.config for the cmd runner or Site Settings section for the Client Application.

### Using Integration Tests Client Application Runner

After openning the Client Application - you will be prompted to enter the url of the website and the credentials for the Back end. When entering your credentials you can specify a membership provider by selecting the _Use membership provider_ checkbox. For example, you can authenticate with users from a _LDAP_ prvovider. If the Membership provider is left blank the default one will be used to authenticate the user. You can use The Site Settings button to explicitly open the dialog and change your settings ( Located in the bottom right corner of the application).

**Before you run the tests you should be logged out of the website.**

You can also use the **Telerik.WebTestRunner.Client.exe.config** or **App.Config** that is part of the  Telerik.WebTestRunner.Client project to edit credentials and url settings:
Example:
```XML
<credentialsConfiguration>
  <credentials>
    <credential username="admin" password ="password" provider="" isActive="true"/>
   </credentials>
</credentialsConfiguration>
```
```XML
<machineSpecificConfigurations>
  <machines>
    <machine name="MACHINENAME" testingInstanceUrl= "http://localhost"/>
   </machines>
 </machineSpecificConfigurations>
```
### Using CMD Integration Tests Runner

The CMD runner allows you to run integration tests through the console application. 

Prerequisites:
The assembly you are testing should be inside the bin folder of the website.

Example:
```xml
 Telerik.WebTestRunner.Cmd.exe run /Url="http://yourhost/" /TraceFilePath="pathWhereResultsXmlGoes" /TimeOutInMinutes=70 /RunName=Test /assemblyname="Telerik.Samples.IntegrationTests"
```
Supported arguments:
* Url - the location of the website which holds the integration tests service.
* TraceFilePath - The log file path, where test results are exported.
* RunName - Name of the current run.
* AssemblyName - Name of the assembly that holds your tests. If you are not going to run tests from separate assembly, don't add assemblyName in your query.
* CategoriesFilter - Supports multiple test categories, so you can execute group of tests instead of entire list. The available categories in Sitefinity CMS are: ModuleBuilder, Data, Core, InlineEditing, Connectors, OpenAccess, Modules, Multisite, ContentApi, SDK, Services, Publishing, Migration, Lighting, RecycleBin, Ssl.
* LoggerType - The CMD runner supports MS TRX format that allows you to integrate it with other systems that can read it like Jenkins CI. If you don't set LoggerType - the default SitefinityXml format is used.
* Different User Support - Both parameters must be used at the same time.
  * User - Specifies the username
  * Role - Specifies the role of the user

**App.config settings**
* Setting execution timeout of the entire run and for single test in the list:

```XML
<runnerConfiguration>
   <runner timeout="60" testTimeout="3"/>
</runnerConfiguration>
```
* Teams and Team Members configuration:

```XML
 <teamsConfiguration>
    <teams>
      <team name="Team 1">
        <members>
          <member fullName="User1" aliases="user1, user 1"></member>
          <member fullName="User2" aliases="user2, user 2"></member>
        </members>
      </team>
    </teams>
 </teamsConfiguration>
```
*  Machine configurations:

```XML
 <machineSpecificConfigurations>
    <machines>
      <machine name="MACHINENAME" testingInstanceUrl="http://localhost"/>
    </machines>
 </machineSpecificConfigurations>
```
*  Credentials configurations:

```XML
 <credentialsConfiguration>
    <credentials>
      <credential username="admin" password ="admin@2" provider="" isActive="true"/>
    </credentials>
 </credentialsConfiguration>
```
### Troubleshooting
 * The integration tests are retrieved by a service with a GET request. You can check if the tests are loaded through your browser using the following url (replace the host with the one you use):
```XML http://localhost/IntegrationTests/TestRunnerService.svc/GetTests ```
 * If you make a single request to the service it caches the test results, so when you are working with categories you should have this in mind.
 * Website restart will clear cache.
 * To check if tests from a categori(ies) are loaded you can use the categories query in the url like shown below:
```XML http://localhost/IntegrationTests/TestRunnerService.svc/GetTests?categories=ModuleBuilder ```
 * You might also have issues if you use a different version than the latest one of the Telerik.WebTestRunner.Server.dll. This is the server part of the service that is referenced inside the integration tests project.
It contains the WCF service contract and other code that returns data to the client – WPF stand alone application or CMD runner.

### Additional resources
Sitefinity CMS documentation:
* [Develop: Use and extend Sitefinity CMS functionality](http://docs.sitefinity.com/develop-create-and-manage-website-content)
* [Tutorial: Create integration tests with Sitefinity CMS web test runner](http://docs.sitefinity.com/tutorial-create-integration-tests-with-sitefinity-web-test-runner)
