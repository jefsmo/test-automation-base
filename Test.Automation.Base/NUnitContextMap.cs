using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

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

            Id = testContext.Test.ID;
            TestName = testContext.Test.Name;
            SafeTestName = NUnitTestBase.RemoveInvalidFileNameChars(TestName);
            MethodName = testContext.Test.MethodName;
            ClassName = testContext.Test.ClassName;
            FullyQualifiedTestClassName = testContext.Test.FullName;
            CurrentDirectory = Directory.GetCurrentDirectory();
            LogDirectory = testContext.WorkDirectory;
            TestBinariesDirectory = testContext.TestDirectory;
            TestResultStatus = NUnitTestBase.GetTestResultStatus(testContext.Result.Outcome.Status);
            Message = testContext.Result.Message;

            Timeout = int.MaxValue; // Default to 'infinite' timeout.
            Category = new List<string>();
            WorkItem = new List<string>();
            Property = new List<KeyValuePair<string, string>>();

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
                        var priority = properties.Get("Priority");
                        if (Enum.IsDefined(typeof(Priority), priority))
                        {
                            Priority = (Priority)priority;
                        }
                        break;
                    case "Timeout":
                        Timeout = (int)properties.Get("Timeout");
                        break;
                    case "Category":
                        Category = properties["Category"].Select(x => x.ToString().Trim()).ToList();
                        break;
                    case "WorkItem":
                        WorkItem = properties["WorkItem"].Select(x => x.ToString().Trim()).ToList();
                        break;
                    default:
                        Property = properties[key].Select(x => new KeyValuePair<string, string>(key, x.ToString())).ToList();
                        break;
                }
            }
        }

        #region TEST ATTRIBUTES
        /// <summary>
        /// Gets or sets the time-out period of a unit test.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets the priority of a unit test. 
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Gets or sets the description of the test. 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the person responsible for maintaining, running, and/or debugging the test. 
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the category of a unit test.
        /// </summary>
        public IList<string> Category { get; set; }
        
        /// <summary>
        /// Gets or sets a work item associated with a test.
        /// </summary>
        public IList<string> WorkItem { get; set; }

        /// <summary>
        /// Gets or sets a test specific property on a method.
        /// </summary>
        public IList<KeyValuePair<string, string>> Property { get; set; }
        #endregion

        #region TEST CONTEXT
        /// <summary>
        /// Gets or sets the test ID.
        /// NUnit: The unique Id of the test.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the test name from TestContext.
        /// NUnit: The name of the test which may or may not be the same as the method name.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the test name with any invalid file name characters replaced by a safe character.
        /// </summary>
        public string SafeTestName { get; set; }

        /// <summary>
        /// Gets or sets the test method name.
        /// NUnit: The name of the method representing the test.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the test class short name.
        /// NUnit: The class name of the test.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified test class name from TestContext.
        /// NUnit: The full name of the test.
        /// </summary>
        public string FullyQualifiedTestClassName { get; set; }

        /// <summary>
        /// Gets or sets the current directory of the executing assembly from Directory.GetCurrentDirectory().
        /// </summary>
        public string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the directory containing files deployed for test run.
        /// MSTest: DeploymentDirectory: Returns the directory for files deployed for the test run.
        ///  NUnit: TestDirectory: Gets the directory containing the current test assembly.
        /// </summary>
        public string TestBinariesDirectory { get; set; }

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
        /// Gets or sets the current test outcome from TestContext mapped to one of the TestResultStatus enum values.
        /// [ Pass | Fail | In Progress | Not Executed | Blocked ]
        /// </summary>
        public TestResultStatus TestResultStatus { get; set; }
        #endregion
    }
}
