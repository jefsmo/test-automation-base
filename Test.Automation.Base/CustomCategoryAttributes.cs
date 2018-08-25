using System;
using NUnit.Framework;

namespace Test.Automation.Base
{
    /// <summary>
    /// Level - UnitTest: verify the functionality of a specific section of code
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UnitTestAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Level - Integration: verify the interfaces between components against a software design.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Level - Component: check the handling of data passed between various units, or subsystem components, beyond full integration testing between those units.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ComponentAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Level - System: validate the software as a whole (software, hardware, and network) against the requirements for which it was built.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SystemAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Type - Smoke: minimal attempts to operate the software, designed to determine whether there are any basic problems that will prevent it from working at all.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SmokeAttribute : CategoryAttribute
    { }

    /// <summary>
    ///  Type - Functional: verify a specific action or function of the code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FunctionalAttribute : CategoryAttribute
    { }

    /// <summary>
    ///  Type - Accessiblity: determine if the contents of the website can be easily accessed by disable people or compliance with standards.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AccessibilityAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Type - Security: degree to which a test item, and associated data and information, are protected.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SecurityAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Type - AdHoc: test intended to find defects that were not found by existing test cases.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AdHocAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Type - Mock: test uses a Mock framework in place of a repository dependency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MockAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Type - Negative: test uses invalid or out of range arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NegativeAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Area - Api: APIs are tested as per API specification.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Area - Database: verifies database functionality.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DatabaseAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Area - Web: verifies the Web UI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WebAttribute : CategoryAttribute
    { }

    /// <summary>
    /// Area - Reports: verifies SSRS reports.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ReportingAttribute : CategoryAttribute
    { }
}
