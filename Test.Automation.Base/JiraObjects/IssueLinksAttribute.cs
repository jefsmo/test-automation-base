using System;
using NUnit.Framework;

namespace Test.Automation.Base
{
    /// <summary>
    /// Custom Property Attribute used to label a test with related work items.
    /// A comma-separated list of references/requirements for the test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class IssueLinksAttribute : PropertyAttribute
    {
        /// <summary>
        /// Constructs a list of references/requirements for the test.
        /// </summary>
        /// <param name="issueLinks"></param>
        public IssueLinksAttribute(params string[] issueLinks)
        {
            foreach (var issue in issueLinks)
            {
                // Test for valid key using regex?
                Properties.Add("IssueLinks", issue.ToUpperInvariant().Trim());
            }
        }
    }
}
