using System;
using System.Collections.Generic;

namespace JobApplications.Models
{
    /// <summary>
    /// Groups information related toi a job posting
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Job unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Job's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of minimum qualifications for the job, each question Id can have a list of
        /// acceptable answers that qualifies or disqualifies a job application
        /// </summary>
        public Dictionary<Question, List<Answer>> Qualifications { get; set; }
    }
}
