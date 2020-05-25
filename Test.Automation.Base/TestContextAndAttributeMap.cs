using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Test.Automation.Base
{
    /// <summary>
    /// Represents an NUnit TestContext mapped to test framework agnostic fields.
    /// </summary>
    public class TestContextAndAttributeMap : ITestAutomationContext, ITestAutomationAttributes
    {
        /// <summary>
        /// Creates a test framework agnostic test context from NUnit TestsContext data.
        /// </summary>
        /// <param name="testContext"></param>
        public TestContextAndAttributeMap(TestContext testContext)
        {
            _ = testContext ?? throw new ArgumentNullException(nameof(testContext));

            Id = testContext.Test.ID;
            TestName = testContext.Test.Name;
            SafeTestName = TestAutomationBase.RemoveInvalidFileNameChars(TestName);
            MethodName = testContext.Test.MethodName;
            ClassName = testContext.Test.ClassName;
            FullName = testContext.Test.FullName;
            CurrentDirectory = Directory.GetCurrentDirectory();
            TestDirectory = testContext.TestDirectory;
            WorkDirectory = testContext.WorkDirectory;
            Message = testContext.Result.Message;
            TestResultStatus = TestAutomationBase.GetTestResultStatus(testContext.Result.Outcome.Status);

            // Extract the property bag from TestContext.
            var properties = testContext.Test.Properties;

            Timeout = (int)(properties.Get("Timeout") ?? int.MaxValue);
            Priority = (Priority)Enum.Parse(typeof(Priority), (properties.Get("Priority") ?? Priority.None).ToString());
            Description = (string)(properties.Get("Description") ?? "Description attribute not set.");
            Author = (string)(properties.Get("Author") ?? "Author attribute not set.");
            Category = properties["Category"].Any() 
                ? string.Join(", ", properties["Category"]) 
                : "Categories attribute not set.";
            IssueLinks = properties["IssueLinks"].Any() 
                ? string.Join(", ", properties["IssueLinks"]) 
                : "IssueLinks attribute not set.";
            //
            // Any new custom properties need to be defined in this class or they will not be mapped.
            // 
        }

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
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the current directory of the executing assembly from Directory.GetCurrentDirectory().
        /// </summary>
        public string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the directory containing files deployed for test run.
        /// MSTest: DeploymentDirectory: Returns the directory for files deployed for the test run.
        ///  NUnit: TestDirectory: Gets the directory containing the current test assembly.
        /// </summary>
        public string TestDirectory { get; set; }

        /// <summary>
        /// Gets or sets the log directory from TestContext.
        /// NUnit: Gets the directory to be used for outputting files created by this test run. 
        /// </summary>
        public string WorkDirectory { get; set; }

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

        #region TEST ATTRIBUTES
        /// <summary>
        /// Gets or sets the time-out period of a unit test.
        /// </summary>
        [DefaultValue(int.MaxValue)]
        public int Timeout { get; }

        /// <summary>
        /// Gets or sets the priority of a unit test. 
        /// </summary>
        [DefaultValue(Priority.None)]
        public Priority Priority { get; }

        /// <summary>
        /// Gets or sets the description of the test. 
        /// </summary>
        [DefaultValue("Description attribute not set.")]
        public string Description { get; }

        /// <summary>
        /// Gets or sets the person responsible for maintaining, running, and/or debugging the test. 
        /// </summary>
        [DefaultValue("Author attribute not set.")]
        public string Author { get; }

        /// <summary>
        /// Gets or sets the category of a unit test.
        /// </summary>
        [DefaultValue("Categories attribute not set.")]
        public string Category { get; }

        /// <summary>
        /// Gets or sets a work item associated with a test.
        /// </summary>
        [DefaultValue("IssueLinks attribute not set.")]
        public string IssueLinks { get; }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Test Context Metadata:".ToUpperInvariant());
            sb.AppendLine($"Test ID\t\t {Id}");
            sb.AppendLine($"Test Name\t\t {TestName}");
            sb.AppendLine($"Safe Test Name\t\t {SafeTestName}");
            sb.AppendLine($"Method Name\t\t {MethodName}");
            sb.AppendLine($"Class Name\t\t {ClassName}");
            sb.AppendLine($"Full Name\t\t {FullName}");
            sb.AppendLine($"Current Dir\t\t {CurrentDirectory}");
            sb.AppendLine($"Test Dir\t\t {TestDirectory}");
            sb.AppendLine($"Work Dir\t\t {WorkDirectory}");
            sb.AppendLine($"Message\t\t {Message}");
            sb.AppendLine($"Test Result\t\t {TestResultStatus}");

            sb.AppendLine("Test Attribute Metadata:".ToUpperInvariant());
            sb.AppendLine($"Timeout\t\t {Timeout.ToString(CultureInfo.InvariantCulture)}");
            sb.AppendLine($"Priority\t\t {Priority}");
            sb.AppendLine($"Description\t\t {Description}");
            sb.AppendLine($"Author\t\t {Author}");
            sb.AppendLine($"Categories\t\t {Category}");
            sb.Append($"Issue Links\t\t {IssueLinks}");

            return sb.ToString();
        }
    }
}
