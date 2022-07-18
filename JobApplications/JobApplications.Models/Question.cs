using System;

namespace JobApplications.Models
{
    /// <summary>
    /// Represents a question to be presented as part of a job
    /// posting, it doesn't include default answers or specific answers since
    /// those might vary depending on the job application
    /// </summary>
    [Serializable()]
    public class Question
    {
        /// <summary>
        /// Question identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The question's description
        /// </summary>
        public string Text { get; set; }
    }
}
