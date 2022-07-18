using System.Collections.Generic;

namespace JobApplications.Models.JsonImport
{
    /// <summary>
    /// The only purpose of this class is to provide and object to serialize the list of job applications
    /// coming from the endpoint, since I didn't want to modify the format of the input since didn't
    /// knew if it was allowed
    /// </summary>
    public class JobApplicationsFromJson
    {
        /// <summary>
        /// Applicant Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Questions from the applicant
        /// </summary>
        public IEnumerable<QuestionsInJobApplicationFromJson> Questions { get; set; }
    }
}
