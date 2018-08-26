namespace Test.Automation.Base
{
    /// <summary>
    /// An interface that desribes the data in the test context.
    /// </summary>
    public interface ITestAutomationContext
    {
        /// <summary>
        /// Gets or sets the unique Id of the test.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the test name from TestContext.
        /// </summary>
        string TestName { get; set; }

        /// <summary>
        /// Gets or sets the test name with any invalid file name characters replaced by a safe character.
        /// </summary>
        string SafeTestName { get; set; }

        /// <summary>
        /// Gets or sets the name of the method representing the test.
        /// </summary>
        string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the test class short name.
        /// </summary>
        string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified test class name from TestContext.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets or sets the current directory of the executing assembly from Directory.GetCurrentDirectory().
        /// </summary>
        string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the directory where the test binaries are located.
        /// NUNIT: TestDirectory: Gets the full path of the directory containing the current test assembly.
        /// </summary>
        string TestDirectory { get; set; }

        /// <summary>
        /// Gets or sets the work directory from TestContext.
        /// NUnit: Gets the full path of the directory to be used for output from this test run. 
        /// </summary>
        string WorkDirectory { get; set; }

        /// <summary>
        /// Gets or sets the message from TestContext or the original test status from the test framework TestContext.
        /// NOTE: Not all test frameworks support message.
        /// </summary>
        string Message { get; set; }     
        
        /// <summary>
        /// Gets or sets the current test outcome from TestContext mapped to one of the enum values.
        /// [ Pass | Fail | In Progress | Not Executed | Blocked ]
        /// </summary>
        TestResultStatus TestResultStatus { get; set; }
    }
}
