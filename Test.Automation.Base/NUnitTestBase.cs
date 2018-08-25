using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Test.Automation.Base
{
    /// <summary>
    /// Represents an abstract base class for logging NUnit TestContext data.
    /// </summary>
    [TestFixture]
    public abstract class NUnitTestBase
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
                MappedContext = new NUnitContextMap(TestContext.CurrentContext);
                LogTestAttributes(MappedContext);
                LogTestContext(MappedContext);
            }
        }

        /// <summary>
        /// Formats and logs data to output window.
        /// </summary>
        /// <param name="logSectionName">The name of the log section.</param>
        /// <param name="objectToLog">The information getting logged into that section.</param>
        public static void WriteLogToOutput(string logSectionName, object objectToLog)
        {
            Console.WriteLine($"{logSectionName.ToUpperInvariant()}");
            Console.WriteLine(JsonConvert.SerializeObject(objectToLog, Formatting.Indented));
            Console.WriteLine($"{new string('=', 80)}");
        }
        
        /// <summary>
        /// Replaces any invalid file name characters with a safe character (default = 'X') to preserve the filename length.
        /// </summary>
        /// <param name="name">The file name to remove invalid characters from.</param>
        /// <param name="safeCharacter">The character to replace the unsafe character with.</param>
        /// <returns>Returns a string.</returns>
        public static string RemoveInvalidFileNameChars(string name, string safeCharacter = "X")
        {
            return string.Join(safeCharacter, name.Split(Path.GetInvalidFileNameChars()));
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

        private static void LogTestAttributes(ITestAutomationContext mappedContext)
        {
            // Log test attribute info.
            var timeout = -1;

            if (mappedContext.Timeout > 0 && mappedContext.Timeout < int.MaxValue)
            {
                timeout = mappedContext.Timeout;
            }

            var attributes = new
            {
                mappedContext.Description,
                mappedContext.Owner,
                Timeout = timeout == -1
                    ? "Infinite"
                    : $"{timeout.ToString("N0")} (ms)",
                Priority = mappedContext.Priority + $" ({(int)mappedContext.Priority})",
                mappedContext.Category,
                mappedContext.WorkItem,
                mappedContext.Property
            };

            WriteLogToOutput("Test Attributes", attributes);
        }

        private static void LogTestContext(ITestAutomationContext mappedContext)
        {
            // Log test context info.
            var context = new
            {
                mappedContext.Id,
                mappedContext.ClassName,
                mappedContext.MethodName,
                mappedContext.TestName,
                mappedContext.TestBinariesDirectory,
                mappedContext.CurrentDirectory,
                mappedContext.LogDirectory
            };

            WriteLogToOutput("Test Context", context);
        }
    }
}

