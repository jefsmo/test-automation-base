using System;

namespace Test.Automation.Base
{
    public class GetCaseResponseDto
    {
        // Default get_case response fields.

        /// <summary>
        /// The ID of the user who created the test case.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// The date/time when the test case was created (as UNIX timestamp).
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The estimate, e.g. "30s" or "1m 45s".
        /// </summary>
        public TimeSpan Estimate { get; set; }

        /// <summary>
        /// The estimate forecast, e.g. "30s" or "1m 45s".
        /// </summary>
        public TimeSpan EstimateForecast { get; set; }

        /// <summary>
        /// The unique ID of the test case.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ID of the milestone that is linked to the test case.
        /// </summary>
        public int MilestoneID { get; set; }

        /// <summary>
        /// The ID of the priority that is linked to the test case.
        /// </summary>
        public int PriorityID { get; set; }

        /// <summary>
        /// A comma-separated list of references/requirements.
        /// </summary>
        public string Refs { get; set; }

        /// <summary>
        /// The ID of the seciton the test case belongs to.
        /// </summary>
        public int SectionID { get; set; }

        /// <summary>
        /// The ID of the suite the test case belongs to.
        /// </summary>
        public int SuiteID { get; set; }

        /// <summary>
        /// The ID of the template (field layout) the test case uses (requires TestRail 5.2 or later).
        /// </summary>
        public int TemplateID { get; set; }

        /// <summary>
        /// The title of the test case.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The ID of the test case type that is linked to the test case.
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// The ID of the user who last updated the test case.
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// The date/time when the test case was last updated (as UNIX timestamp).
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        // 
        // Add custom test case fields below.
        // 
    }
}
