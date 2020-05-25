using NUnit.Framework;
using Test.Automation.Base;

namespace Test.Automation.BaseTests
{
    [TestFixture]
    public class UnitTest1 : TestAutomationBase
    {
        [Test,
            Timeout(60000),
            Priority(Priority.High),
            Description("This test fails and has [Test] attributes."),
            Author("Your Name"),
            Categories(TestLevel.Component, TestType.Functional, TestArea.None),
            IssueLinks("jra-123", "jra-456", "jra-789"),
            Property("Bug", "FOO-42"), 
            Property("Bug", "FOO-43"), 
            Property("ID", "BAR-42")]
        public void AllAttributes_ShouldFail()
        {
            Assert.That(21 + 21, Is.EqualTo(41));
        }

        [Test(
            Author = "Your Name",
            Description = "This test passes and has [Test] attributes.",
            TestOf = typeof(int)),
            Priority(Priority.Normal),
            Categories(TestLevel.None, TestType.Functional, TestArea.Api),
            IssueLinks("jqa-123", "JQA-456", "jQa-789"),
            Property("ID", "BAR-42"),
            Property("Value", "100")]
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
