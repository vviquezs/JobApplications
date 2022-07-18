using System;
using System.Collections.Generic;

namespace JobApplications.Models
{
    /// <summary>
    /// Represents a job application
    /// </summary>
    [Serializable()]
    public class JobApplication
    {
        /// <summary>
        /// The job information
        /// </summary>
        public Job Job { get; set; }

        /// <summary>
        /// The applicant information
        /// </summary>
        public Applicant Applicant { get; set; }

        /// <summary>
        /// The list of questions and answers provided by the applicant
        /// </summary>
        public Dictionary<int, Answer> AnsweredQuestions { get; set; }
    }
}
