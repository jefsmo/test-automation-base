﻿using System.ComponentModel;

namespace Test.Automation.Base
{
    /// <summary>
    /// Represents a test priority level.
    /// [ Unknown = 0 | High | Normal | Low ]
    /// </summary>
    public enum TestPriority
    {
        /// <summary>
        /// Default priority value.
        /// </summary>
        [Description("Unknown")]
        Unknown = 0,

        /// <summary>
        /// High priority.
        /// </summary>
        [Description("High")]
        High,

        /// <summary>
        /// Normal priority.
        /// </summary>
        [Description("Normal")]
        Normal,

        /// <summary>
        /// Low priority.
        /// </summary>
        [Description("Low")]
        Low,
    }
}