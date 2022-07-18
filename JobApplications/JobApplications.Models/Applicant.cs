using System;

namespace JobApplications.Models
{
    /// <summary>
    /// Groups information for a job applicant such as name, identification, etc.
    /// </summary>
    [Serializable()]
    public class Applicant
    {
        /// <summary>
        /// Unique identifier for the applicant, defined as string to handle
        /// different formats of identification
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the applicant
        /// </summary>
        public string Name { get; set; }
    }
}
