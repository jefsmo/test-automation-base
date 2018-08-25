using System.Collections.Generic;

namespace Test.Automation.Base
{
    /// <summary>
    /// An interface that describes the data in the test attributes.
    /// </summary>
    public interface ITestAutomationAttributes
    {
        /// <summary>
        /// Gets or sets the description of the test. 
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets  the person responsible for maintaining, running, and/or debugging the test. 
        /// </summary>
        string Owner { get; set; }

        /// <summary>
        /// Gets or sets  the time-out period of a unit test.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Gets or sets  the priority of a unit test. 
        /// </summary>
        Priority Priority { get; set; }

        /// <summary>
        /// Gets or sets  the Class that is used to specify the category of a unit test.
        /// </summary>
        IList<string> Category { get; set; }

        /// <summary>
        /// Gets or sets  a list of work items associated with a test.
        /// </summary>
        IList<string> WorkItem { get; set; }

        /// <summary>
        /// Gets or sets  a test specific property on a method.
        /// </summary>
        IList<KeyValuePair<string, string>> Property { get; set; }
    }
}
