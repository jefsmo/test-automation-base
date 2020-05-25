using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Diagnostics;
using System.IO;

namespace Test.Automation.Base
{
    /// <summary>
    /// Represents an abstract base class for logging NUnit TestContext data.
    /// </summary>
    [TestFixture]
    public abstract class TestAutomationBase
    {
        /// <summary>
        /// Gets or sets the NUnit ITestAutomationContext of the current test method.
        /// </summary>
        public ITestAutomationContext MappedContext { get; set; }

        /// <summary>
        /// Logs the NUnit TestContext data if test does not pass or is run in debug mode.
        /// </summary>
        [TearDown]
        public void TestAutomationBaseCleanup()
        {
            if (!(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                || Debugger.IsAttached)
            {
                MappedContext = new TestContextAndAttributeMap(TestContext.CurrentContext);
                LogContextAndAttributesToOutput(MappedContext);
            }
        }

        /// <summary>
        /// Formats and logs test context and test attribute metadata to output window.
        /// </summary>
        /// <param name="context">The information getting logged into that section.</param>
        public static void LogContextAndAttributesToOutput(ITestAutomationContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));

            Console.WriteLine(context.ToString());
            Console.WriteLine($"{new string('=', 40)}");
        }
        
        /// <summary>
        /// Replaces any invalid file name characters with a safe character (default = 'X') to preserve the filename length.
        /// </summary>
        /// <param name="name">The file name to remove invalid characters from.</param>
        /// <param name="safeCharacter">The character to replace the unsafe character with.</param>
        /// <returns>Returns a string.</returns>
        public static string RemoveInvalidFileNameChars(string name, string safeCharacter = "X")
        {
            return string.Join(safeCharacter, name?.Split(Path.GetInvalidFileNameChars()));
        }

        /// <summary>
        /// Returns the TestStatus mapped to one of the TestResultStatus values.
        /// </summary>
        /// <param name="currentTestStatus">The result of the test.</param>
        /// <returns>Returns a test framework agnostic test result.</returns>
        public static TestResultStatus GetTestResultStatus(TestStatus currentTestStatus)
        {
            switch (currentTestStatus)
            {
                case TestStatus.Passed:
                    return TestResultStatus.Pass;
                case TestStatus.Failed:
                    return TestResultStatus.Fail;
                case TestStatus.Skipped:
                    return TestResultStatus.NotExecuted;
                case TestStatus.Warning:
                case TestStatus.Inconclusive:
                    return TestResultStatus.Blocked;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentTestStatus), currentTestStatus, null);
            }
        }
    }
}

