namespace Test.Automation.Base
{
    /// <summary>
    /// An interface that describes the data in the test attributes.
    /// </summary>
    public interface ITestAutomationAttributes
    {
        /// <summary>
        /// Gets or sets  the time-out period of a unit test.
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// Gets or sets  the priority of a unit test. 
        /// </summary>
        Priority Priority { get; }

        /// <summary>
        /// Gets or sets the description of the test. 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets or sets  the person responsible for maintaining, running, and/or debugging the test. 
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets or sets  the Class that is used to specify the category of a unit test.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets or sets  a list of work items associated with a test.
        /// </summary>
        string IssueLinks { get; }
    }
}
