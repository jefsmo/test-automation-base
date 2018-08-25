using System;
using NUnit.Framework;
using Test.Automation.Base;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1 : NUnitTestBase
    {
        [Test]
        public void TestMethod1()
        {
        }

        [Test,
            Timeout(300000),
            Priority(Priority.High),
            Description("All attributes test."),
            Integration, Functional, Api,
            WorkItem("1"), WorkItem("2"), WorkItem("3"),
            Author("automation"),
            Property("prop", "1"), Property("prop", "2")]
        public void TestMethod2()
        { }

        [TestCase(1, 1, 2)]
        [TestCase(2, 2, 4, Author = "automation", Category = "Integration, Functional, Api", Description = "Paramaterized test with all attributes.", TestName = "MyParamTest")]
        public void TestMethod3(int op1, int op2, int result)
        { }
    }
}
