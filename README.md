# Test.Automation.Base

**README.md**

Automatically logs test attribute and test context data to the output window.  

|Test Mode|Result|Log to Output|
|---|---|---|
|Run|Pass|no|
|Run|Fail|yes|
|Debug|Pass|yes|
|Debug|Fail|yes|

 - In general, logging can add a significant performance penalty to a passing test.
 - When a test suite contains hundreds of tests, this can add many minutes or even hours to the test run.
 - This framework only logs when tests fail or are run in debug mode.

## Release Notes
|Date|Description|
|---|---|
|2018-08-25|Bug fixes; refactoring|
|2018-07-01|Bug fixes <br> Update README|
|2018-02-18|Bug fixes|
|2017-12-05|Initial Release|

## Contents
[NUnit Test Project Workflow](#nunit-test-project-workflow)  
[Viewing Local Packages](#viewing-local-packages)  
[Octopack Reference](#octopack-reference)  
[Troubleshooting](#troubleshooting)  

## NUnit Test Project Workflow
- Ensure your test class inherits from the base class **`NUnitTestBase`**
- Ensure you have installed NUnit and NUnit3TestAdapter NuGet packages.
- If the test is run in debug mode or if the test fails, the output window displays the test log.
- If the test passes and is not run in debug mode, nothing is logged to the output window.

### Example Tests

```csharp
using NUnit.Framework;
using Test.Automation.Base;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1 : NUnitTestBase
    {
        [Test]
        public void NoAttributes_ShouldFail()
        {
            Assert.That(4 + 1, Is.EqualTo(4));
        }

        [Test,
            Timeout(60000),
            Priority(Priority.High),
            Description("This test fails and has [Test] attributes."),
            Author("Your Name"),
            Integration, Negative, Web,
            Property("Bug", "FOO-42"), Property("Bug", "FOO-43"), Property("ID", "BAR-42"),
            WorkItem("123"), WorkItem("456"), WorkItem("789")]
        public void AllAttributes_ShouldFail()
        {
            Assert.That(21 + 21, Is.EqualTo(41));
        }

        [Test,
            Timeout(int.MaxValue),
            Priority(Priority.Normal),
            Description("This test passes and has [Test] attributes."),
            Author("Your Name"),
            UnitTest, Functional, Database,
            Property("ID", "BAR-42"), Property("Value", "100"),
            WorkItem("123"), WorkItem("456"), WorkItem("789")]
        public void AllAttributes_ShouldPass()
        {
            Assert.That(21 + 21, Is.EqualTo(42));
        }

        [TestCase(2, 3, Author = "Your Name", Category = "Integration, Smoke, Web", Description = "This test fails and has [TestCase] attributes.", TestName = "ParameterizedTestCase_ShouldFail")]
        [TestCase(-5, 9)]
        public void ParameterizedTestCase(int first, int second)
        {
            Assert.That(first + second, Is.EqualTo(4));
        }
    }
}
```
  
### Example Test Output

```json
Test with all test attributes:
{
  "Id": "0-1002",
  "TestName": "AllAttributes_ShouldFail",
  "SafeTestName": "AllAttributes_ShouldFail",
  "MethodName": "AllAttributes_ShouldFail",
  "ClassName": "UnitTestProject2.UnitTest1",
  "FullName": "UnitTestProject2.UnitTest1.AllAttributes_ShouldFail",
  "CurrentDirectory": "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE",
  "TestDirectory": "C:\\Source\\Repos\\test-automation-base\\UnitTestProject2\\bin\\Debug",
  "WorkDirectory": "C:\\Source\\Repos\\test-automation-base\\UnitTestProject2\\bin\\Debug",
  "Message": "  Expected: 41\r\n  But was:  42\r\n",
  "TestResultStatus": "Fail",
  "Timeout": 60000,
  "Priority": "High",
  "Description": "This test fails and has [Test] attributes.",
  "Owner": "Your Name",
  "Category": [
    "Integration",
    "Negative",
    "Web"
  ],
  "WorkItem": [
    "123",
    "456",
    "789"
  ],
  "Property": [
    {
      "Key": "Bug",
      "Value": "FOO-42"
    },
    {
      "Key": "Bug",
      "Value": "FOO-43"
    },
    {
      "Key": "ID",
      "Value": "BAR-42"
    }
  ]
}
Test with no test attributes:
{
  "Id": "0-1001",
  "TestName": "NoAttributes_ShouldFail",
  "SafeTestName": "NoAttributes_ShouldFail",
  "MethodName": "NoAttributes_ShouldFail",
  "ClassName": "UnitTestProject2.UnitTest1",
  "FullName": "UnitTestProject2.UnitTest1.NoAttributes_ShouldFail",
  "CurrentDirectory": "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE",
  "TestDirectory": "C:\\Source\\Repos\\test-automation-base\\UnitTestProject2\\bin\\Debug",
  "WorkDirectory": "C:\\Source\\Repos\\test-automation-base\\UnitTestProject2\\bin\\Debug",
  "Message": "  Expected: 4\r\n  But was:  5\r\n",
  "TestResultStatus": "Fail"
}

```
  
## Viewing Local Packages
- Install NuGet Package Explorer to view local packages.  
- [NuGetPackageExplorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer)

## Octopack Reference
#### Create a Local NuGet Package with OctoPack
- Add a `.nuspec` file to each project in the solution that you want to package with NuGet.
- The `.nuspec` file name **must be the same name as the project** with the `.nuspec` extension
- Open a '`Developer Command Prompt for VS2017`' command window.
- Navigate to the solution or project that you want to OctoPack.
- Run the following command:

#### MSBuild Octopack Command

```text
MSBUILD Test.Automation.Base.csproj /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=C:\Packages /p:OctoPackPackageVersion=1.0.0
```
 
##### MSBUILD OctoPack Command Syntax
|Switch|Value|Definition|
|-----|-----|-----|
|`/t:Rebuild`|  |MSBUILD Rebuilds the project(s).|
|`/p:Configuration=`|`Release`|Creates and packages a Release build.|
|`/p:RunOctoPack=`|`true`|Creates packages with Octopack using the .nuspec file layout.|
|`/p:OctoPackPackageVersion=`|`1.0.0`|Updates Package Version.|
|`/p:OctoPackPublishPackageToFileShare=`|`C:\Packages`|Copies packages to local file location.|
    
##### Other OctoPack Options:

|Switch|Value|Description|
|-----|-----|-----|
|`/p:Configuration=`|`[ Release | Debug ]`|The build configuration|
|`/p:RunOctoPack=`|`[ true | false ]`|Enable or Disable OctoPack|
|`/p:OctoPackPackageVersion=`|`1.2.3`|Version number of the NuGet package. By default, OctoPack gets the version from your assembly version attributes. Set this parameter to use an explicit version number.|
|`/p:OctoPackPublishPackageToFileShare=`|`C:\Packages`|Copies packages to the specified directory.|
|`/p:OctoPackPublishPackageToHttp=`|`http://my-nuget-server/api/v2/package`| Pushes the package to the NuGet server|
|`/p:OctoPackPublishApiKey=`|`ABCDEFGMYAPIKEY`|API key to use when publishing|
|`/p:OctoPackNuGetArguments=`| `-Verbosity detailed`|Use this parameter to specify additional command line parameters that will be passed to NuGet.exe pack.|
|`/p:OctoPackNuGetExePath=`|`C:\MyNuGetPath\`|OctoPack comes with a bundled version of NuGet.exe. Use this parameter to force OctoPack to use a different NuGet.exe instead.|
|`/p:OctoPackNuSpecFileName=`|`<C#/VB_ProjectName>.nuspec`|The NuSpec file to use.|

## Troubleshooting
TBD
