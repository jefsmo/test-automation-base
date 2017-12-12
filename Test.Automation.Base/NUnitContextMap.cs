using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Test.Automation.Base
{
    /// <summary>
    /// Represents an NUnit TestContext mapped to test framework agnostic fields.
    /// </summary>
    public class NUnitContextMap : ITestAutomationContext
    {
        /// <summary>
        /// Creates a test framework agnostic test context from NUnit TestsContext data.
        /// </summary>
        /// <param name="testContext"></param>
        public NUnitContextMap(TestContext testContext)
        {
            // Extract the property bag from TestContext.
            var properties = testContext.Test.Properties;

            TestName = testContext.Test.Name;
            ClassName = testContext.Test.ClassName;
            CurrentDirectory = Directory.GetCurrentDirectory();
            Description = "Unknown";
            FullyQualifiedTestClassName = testContext.Test.FullName;
            Id = testContext.Test.ID;
            LogDirectory = testContext.WorkDirectory;
            Message = testContext.Result.Message;
            MethodName = testContext.Test.MethodName;
            Owner = "Unknown";
            Priority = TestPriority.Unknown;
            SafeTestName = RemoveInvalidFileNameChars(TestName, "X");
            TestBinariesDirectory = testContext.TestDirectory;
            TestResultStatus = GetTestResultStatus(testContext.Result.Outcome.Status);
            var testcategories = new List<TestCategory>
            {
                TestCategory.Unknown
            };
            var testproperties = new List<KeyValuePair<string, string>>();
            Timeout = int.MaxValue;
            var workitems = new List<int>();

            // Get the values when the attribute is provided.
            foreach (var key in properties.Keys)
            {
                switch (key)
                {
                    case "Description":
                        Description = (string)properties.Get("Description");
                        break;
                    case "Author":
                        Owner = (string)properties.Get("Author");
                        break;
                    case "Priority":
                        var priority = (TestPriority)(Enum.Parse(typeof(TestPriority), properties.Get("Priority").ToString()));
                        if (priority < 0)
                        {
                            Priority = TestPriority.Unknown;
                        }
                        else if (priority > TestPriority.Low)
                        {
                            Priority = TestPriority.Unknown;
                        }
                        else
                        {
                            Priority = priority;
                        }
                        break;
                    case "Timeout":
                        Timeout = (int)properties.Get("Timeout");
                        break;
                    case "Category":
                        if (properties["Category"].Count > 0)
                        {
                            testcategories.Clear();
                            foreach (var item in properties["Category"])
                            {
                                testcategories.Add((TestCategory)Enum.Parse(typeof(TestCategory), item.ToString()));
                            }
                        }
                        break;
                    case "WorkItem":
                        foreach (int item in properties["WorkItem"])
                        {
                            workitems.Add(item);
                        }
                        break;
                    default:
                        testproperties.Add(new KeyValuePair<string, string>(key, Convert.ToString(properties.Get(key))));
                        break;
                }
            }
            TestCategories = testcategories;
            TestProperties = testproperties.Count == 0
                ? new List<KeyValuePair<string, string>>{ new KeyValuePair<string, string>("Unknown", "Unknown") }
                : testproperties;
            WorkItems = workitems;
        }

        #region TEST ATTRIBUTES

        /// <summary>
        /// Gets or sets the description of the test. 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the person responsible for maintaining, running, and/or debugging the test. 
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the priority of a unit test. 
        /// </summary>
        public TestPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the category of a unit test.
        /// </summary>
        public IEnumerable<TestCategory> TestCategories { get; set; }

        /// <summary>
        /// Gets or sets a test specific property on a method.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> TestProperties { get; set; }

        /// <summary>
        /// Gets or sets the time-out period of a unit test.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets a work item associated with a test.
        /// </summary>
        public IEnumerable<int> WorkItems { get; set; }

        #endregion

        #region TEST CONTEXT

        /// <summary>
        /// Gets or sets the test class short name.
        /// NUnit: The class name of the test.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the current directory of the executing assembly from Directory.GetCurrentDirectory().
        /// </summary>
        public string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified test class name from TestContext.
        /// NUnit: The full name of the test.
        /// </summary>
        public string FullyQualifiedTestClassName { get; set; }

        /// <summary>
        /// Gets or sets the test ID.
        /// NUnit: The unique Id of the test.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the log directory from TestContext.
        /// NUnit: Gets the directory to be used for outputting files created by this test run. 
        /// </summary>
        public string LogDirectory { get; set; }

        /// <summary>
        /// Gets or sets the message from TestContext or the original test status from the test framework TestContext.
        /// NUnit: Gets the message associated with a test failure or with not running the test.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the test method name.
        /// NUnit: The name of the method representing the test.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the test name with any invalid file name characters replaced by a safe character.
        /// </summary>
        public string SafeTestName { get; set; }

        /// <summary>
        /// Gets or sets the directory containing files deployed for test run.
        /// MSTest: DeploymentDirectory: Returns the directory for files deployed for the test run.
        ///  NUnit: TestDirectory: Gets the directory containing the current test assembly.
        /// </summary>
        public string TestBinariesDirectory { get; set; }

        /// <summary>
        /// Gets or sets the test name from TestContext.
        /// NUnit: The name of the test which may or may not be the same as the method name.
        /// </summary>
        public string TestName { get; set; }
        
        /// <summary>
        /// Gets or sets the current test outcome from TestContext mapped to one of the TestResultStatus enum values.
        /// [ Pass | Fail | In Progress | Not Executed | Blocked ]
        /// </summary>
        public TestResultStatus TestResultStatus { get; set; }
        
        #endregion

        /// <summary>
        /// Replaces any invalid file name characters with an 'X' to preserve the filename length.
        /// </summary>
        /// <param name="name">The file name to remove invalid characters from.</param>
        /// <returns>Returns a string.</returns>
        public string RemoveInvalidFileNameChars(string name, string safeCharacter)
        {
            return string.Join(safeCharacter, name.Split(Path.GetInvalidFileNameChars()));
        }

        /// <summary>
        /// Returns the TestStatus mapped to one of the TestResultStatus values.
        /// </summary>
        /// <param name="currentTestStatus">The result of the test.</param>
        /// <returns>Returns a test framework agnostic test result.</returns>
        public TestResultStatus GetTestResultStatus(TestStatus currentTestStatus)
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
