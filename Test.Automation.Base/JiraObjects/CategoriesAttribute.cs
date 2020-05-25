using NUnit.Framework;
using System;

namespace Test.Automation.Base
{
    /// <summary>
    /// Designates the test level.
    /// </summary>
    public enum TestLevel
    {
        /// <summary>
        /// No TestLevel assigned.
        /// </summary>
        None = 0,

        /// <summary>
        /// Level - Component: check the handling of data passed between various units, or subsystem components, beyond full integration testing between those units.
        /// </summary>
        Component,

        /// <summary>
        /// Level - Integration: verify the interfaces between components against a software design.
        /// </summary>
        Integration,

        /// <summary>
        /// Level - System: validate the software as a whole (software, hardware, and network) against the requirements for which it was built.
        /// </summary>
        System,

        /// <summary>
        /// Level - UnitTest: verify the functionality of a specific section of code
        /// </summary>
        UnitTest
    }

    /// <summary>
    /// Designates the test type.
    /// </summary>
    public enum TestType
    {
        /// <summary>
        /// No TestType assigned.
        /// </summary>
        None = 0,

        /// <summary>
        /// Type - Accessiblity: determine if the contents of the website can be easily accessed by disable people or compliance with standards.
        /// </summary>
        Accessibility,

        /// <summary>
        /// Type - AdHoc: test intended to find defects that were not found by existing test cases.
        /// </summary>
        AdHoc,

        /// <summary>
        /// Type - Functional: verify a specific action or function of the code.
        /// </summary>
        Functional,

        /// <summary>
        /// Type - Mock: test uses a Mock framework in place of a repository dependency.
        /// </summary>
        Mock,

        /// <summary>
        /// Type - Negative: test uses invalid or out of range arguments.
        /// </summary>
        Negative,

        /// <summary>
        /// Type - Security: degree to which a test item, and associated data and information, are protected.
        /// </summary>
        Security,

        /// <summary>
        /// Type - Smoke: minimal attempts to operate the software, designed to determine whether there are any basic problems that will prevent it from working at all.
        /// </summary>
        Smoke
    }

    /// <summary>
    /// Designates the test area.
    /// </summary>
    public enum TestArea
    {
        /// <summary>
        /// No TestArea assigned.
        /// </summary>
        None = 0,

        /// <summary>
        /// Area - Api: APIs are tested as per API specification.
        /// </summary>
        Api,

        /// <summary>
        /// Area - Database: verifies database functionality.
        /// </summary>
        Database,

        /// <summary>
        /// Area - Reports: verifies SSRS reports.
        /// </summary>
        Reports,

        /// <summary>
        /// Area - Web: verifies the Web UI.
        /// </summary>
        Web
    }

    /// <summary>
    /// Assign categories to a test for level, type and area.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CategoriesAttribute : PropertyAttribute
    {
        public CategoriesAttribute(TestLevel testLevel = TestLevel.None, TestType testType = TestType.None, TestArea testArea = TestArea.None)
        {
            if (testLevel != TestLevel.None)
            {
                Properties.Add("Category", testLevel);
            }

            if (testType != TestType.None)
            {
                Properties.Add("Category", testType);
            }

            if (testArea != TestArea.None)
            {
                Properties.Add("Category", testArea);
            }
        }
    }
}
