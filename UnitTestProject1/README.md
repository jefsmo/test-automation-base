# Test.Automation.Base
**README.md**
## Overview

## MSBuild Octopack Command
```
MSBUILD test-automation-base.sln /t:Build /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=C:\Packages /p:OctoPackPackageVersion=1.0.0 /fl
```


## NUnit Test Project Workflow

## Examples
```
using NUnit.Framework;
using Test.Automation.Base;
using Test.Automation.Base.NUnitBase;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1 : NUnitTestBase
    {
        [Test]
        public void Fail_NoTestAttributes()
        {
            Assert.That(1 + 4, Is.EqualTo(4));
        }

        [Test,
            Author("Your Name"),
            Description("This test fails and has [Test] attributes."),
            Property("Priority", (int)TestPriority.High),
            Timeout(6000),
            Property("Bug", "FOO-42"),
            Property("WorkItem", 123),
            Property("WorkItem", 456),
            Property("WorkItem", 789),
            Integration, Smoke, Web]
        public void Fail_WithTestAttributes()
        {
            Assert.That(42, Is.EqualTo(41));
        }

        [Test,
            Author("Your Name"),
            Description("This test passes and has [Test] attributes."),
            Property("Priority", (int)TestPriority.Normal),
            Timeout(int.MaxValue),
            Property("ID", "BAR-42"),
            Property("WorkItem", 123),
            Property("WorkItem", 456),
            Property("WorkItem", 789),
            UnitTest, Functional, Database]
        public void Pass_WithTestAttributes()
        {
            Assert.That(42, Is.EqualTo(42));
        }

        [TestCase(2, 3, Author = "Your Name", Description = "This test fails and has [TestCase] attributes.", Category = "Integration, Smoke, Web", TestName = "Fail_WithTestCaseAttr")]
        [TestCase(-5, 9, Author = "Your Name", Category = "Integration, Smoke, Web", Description = "This test passes and has [TestCase] attributes.", TestName = "Pass_WithTestCaseAttr")]
        public void TestMulti_TestCaseAttributes(int first, int second)
        {
            Assert.That(first + second, Is.EqualTo(4));
        }
    }
}
```
![Fail_WithTestAttributes.png](Fail_WithTestAttributes.png)
![Pass_WithTestAttributes.png](Pass_WithTestAttributes.png)
![Fail_NoTestAttributes.png](Fail_NoTestAttributes.png)


## Troubleshooting
