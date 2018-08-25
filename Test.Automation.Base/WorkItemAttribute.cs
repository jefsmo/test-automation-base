using System;
using NUnit.Framework;

namespace Test.Automation.Base
{
    /// <summary>
    /// Custom Property Attribute used to label a test with related work items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class WorkItemAttribute : PropertyAttribute
    {
        /// <summary>
        /// Constructs a WorkItem attribute.
        /// </summary>
        /// <param name="workItem"></param>
        public WorkItemAttribute(string workItem)
            : base(workItem) { }
    }
}
