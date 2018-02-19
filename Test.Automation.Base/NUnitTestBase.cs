using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

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
            if (!(TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
                || Debugger.IsAttached)
            {
                MappedContext = new NUnitContextMap(TestContext.CurrentContext);
                LogTestAttributes(MappedContext);
                LogTestContext(MappedContext);
            }
        }

        private static void LogTestAttributes(ITestAutomationContext mappedContext)
        {
            var timeout = int.MaxValue;
            if (mappedContext.Timeout <= 0 || mappedContext.Timeout == int.MaxValue)
            {
                timeout = -1;
            }
            else
            {
                timeout = mappedContext.Timeout / 6000;
            }

            var attributes = new Dictionary<string, string>
            {
                {
                    "Owner",
                    mappedContext.Owner
                },
                {
                    "Description",
                    mappedContext.Description
                },
                {
                    "Timeout", timeout == -1
                    ? "Infinite"
                    : (timeout) == 1
                        ? $"{timeout} (second)"
                        : $"{timeout} (seconds)"
                },
                {
                    "Test Priority",
                    mappedContext.Priority.ToDescription()
                },
                {
                    "Test Category",
                    mappedContext.TestCategories
                    .Select(x => x.ToDescription())
                    .Aggregate((x, next) => x + ", " + next)
                },
                {
                    "Test Property",
                    string.Join(", ", mappedContext.TestProperties)
                },
                {
                    "Work Item",
                    string.Join(", ",
                    mappedContext.WorkItems.Count() == 0
                    ? new List<string>{ "Unknown"}
                    : mappedContext.WorkItems.Select(x => x.ToString()))
                }
            };
            WriteLogToOutput("Test Attributes", attributes);
        }

        private static void LogTestContext(ITestAutomationContext mappedContext)
        {
            // Log test context info.
            var context = new Dictionary<string, string>
            {
                {"Unique ID", mappedContext.Id },
                {"Class Name", mappedContext.ClassName },
                {"Method Name", mappedContext.MethodName },
                {"Test Name", mappedContext.TestName },
                {"Binaries Dir", mappedContext.TestBinariesDirectory},
                {"Deployment Dir", mappedContext.CurrentDirectory},
                {"Logs Dir", mappedContext.LogDirectory},
            };
            WriteLogToOutput("Test Context", context);
        }

        /// <summary>
        /// Formats and logs data to output window.
        /// Related data is segregated into separate log sections.
        /// </summary>
        /// <param name="logSectionName">The name of the log section.</param>
        /// <param name="logInfo">The information getting logged into that section.</param>
        public static void WriteLogToOutput(string logSectionName, Dictionary<string, string> logInfo)
        {
            Console.WriteLine($"{logSectionName.ToUpper()}");

            foreach (var kvp in logInfo)
            {
                Console.WriteLine($"{kvp.Key,-25}\t{kvp.Value,-30}");
            }
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
    }
}

