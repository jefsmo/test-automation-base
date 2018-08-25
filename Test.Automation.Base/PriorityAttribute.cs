using System;
using NUnit.Framework;

namespace Test.Automation.Base
{
    /// <summary>
    /// Custom Property Attribute used to denote test case priority.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute : PropertyAttribute
    {
        /// <summary>
        /// Constructs a Priority attribute.
        /// </summary>
        /// <param name="priority"></param>
        public PriorityAttribute(Priority priority)
            : base(priority) { }

    }

    /// <summary>
    /// Represents a test priority level.
    /// [ High | Normal | Low ]
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Default priority - Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// High priority.
        /// </summary>
        High,

        /// <summary>
        /// Normal priority.
        /// </summary>
        Normal,

        /// <summary>
        /// Low priority.
        /// </summary>
        Low,
    }
}
