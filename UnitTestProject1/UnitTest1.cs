using NUnit.Framework;
using Test.Automation.Base;

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
            Property("Priority", (int)TestPriority.Low),
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
