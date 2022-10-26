Progress.Sitefinity.Samples.IntegrationTests
===========================================

## Integration Tests Sample using MSTest v2 framework

The Integration Tests sample project is an example how you can write integration tests in Sitefinity using the well known MSTest v2 framework, so you can easily run the tests using Visual Studio Test Explorer UI or through vstest.console.exe command line.


### Requirements

* Sitefinity CMS license
* .NET Framework 4.8
* Visual Studio 2019 or later
* Microsoft SQL Server 2012 or later versions

### Run the sample
1. Build the project
2. Host and setup the Sitefinity web application in IIS
3. OAuth client must be configured for your project which is required for the sitefinity integration tests service. [More info](https://www.progress.com/documentation/sitefinity-cms/configure-oauth2)
4. Update the **IntegrationTestsSettings.json** to match your Sitefinity application settings
5. Run the tests using [Visual Studio Test Explorer](https://learn.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2022) UI or using the [vstest.console.exe](https://learn.microsoft.com/en-us/visualstudio/test/vstest-console-options?view=vs-2022) command line tool.

### How to write integration tests for Sitefinity CMS?
1. Create a new **public** class in your Integration Tests project which inherits from ```SitefinityIntegrationTestsBase``` class.
2. Place the ```[TestClass]``` attribute on top of the class.
3. Implement a public void method that reporesents your test.
4. Put a ```[SitefinityIntegrationTest]``` attribute on top of the method that represents your test.
5. Put a ```[Description("...")]``` attribute on top of the method and briefly describe the test purpose.
6. Implement the test. The Implementation is identical to that of an unit test (except that you have everything at your disposal as you would in a Sitefinity CMS page)
7. Note that the attributes for setup logic and cleanup logic are handled from the base class SitefinityIntegrationTestsBase. Therefore, if you implement some common setup and cleanup logic, you need to override the following methods:
 * ClassInitialize()
Note that this method must have access to static variables only. The method is executed once before all tests in the test class.
 * ClassCleanup()
Note that this method must have access to static variables only. The method is executed once before all tests in the test class. Executes always, even on test or setup failures.
 * TestInitialize()
This method is executed before each test. 
 * TestCleanup()
This method is executed after each test. Executes always, even on test or setup failures.

Example:
```c#
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestIntegration.Framework.MSTest;
using Telerik.Sitefinity.TestUtilities;
 
namespace Telerik.Sitefinity.TestIntegration.MSTest
{
    /// <summary>
    /// Sample integration tests with MSTest v2
    /// </summary>
    [TestClass]
    public class IntegrationTestsExample : SitefinityIntegrationTestsBase
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void ClassInitialize()
        {
            // Executed once before all tests in the test class
            // Must access only static variables
            // Class initialize logic
        }
 
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void ClassCleanup()
        {
            // Executed once after all tests in the test class
            // Must access only static variables
            // Class cleanup logic
        }
 
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void TestInitialize()
        {
            // Executed before each test
            // Test initialize logic
        }
 
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void TestCleanup()
        {
            // Executed after each test
            // Test cleanup logic
        }
 
        /// <summary>
        /// Sample test
        /// </summary>
        [SitefinityIntegrationTest]
        [TestCategory(TestCategories.Common)]
        [Description("Test description goes here.")]
        [Owner(TestAuthor.ToolingTeam)]
        public void SampleTest() 
        {
            // Test logic
        }      
    }
}
```

### Additional resources
* [Sitefinity docs](https://www.progress.com/documentation/sitefinity-cms/create-integration-tests-with-mstest-v2-framework)
* [MSTest framework in unit tests](https://learn.microsoft.com/en-us/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022)
* [MSTest v2 on GitHub](https://github.com/microsoft/testfx)
* [Unit Testing with MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest?WT.mc_id=ondotnet-c9-cxa)
* [MSTest vs framework docs](https://github.com/microsoft/testfx-docs)
