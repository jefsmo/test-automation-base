using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
                LogObjectToOutput("Test Data", MappedContext, new StringEnumConverter());
            }
        }

        /// <summary>
        /// Formats and logs data to output window.
        /// </summary>
        /// <param name="logSectionName">The name of the log section.</param>
        /// <param name="objectToLog">The information getting logged into that section.</param>
        public static void LogObjectToOutput(string logSectionName, object objectToLog, params JsonConverter[] converters)
        {
            var json = JsonConvert.SerializeObject(
                objectToLog,
                Formatting.Indented,
                new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = converters
            });

            Console.WriteLine($"{logSectionName.ToUpperInvariant()}");
            Console.WriteLine(json);
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

