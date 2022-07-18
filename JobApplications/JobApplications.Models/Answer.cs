using System;
using System.Text.Json.Serialization;

namespace JobApplications.Models
{
    /// <summary>
    /// Represents an answer to a given question
    /// </summary>
    [Serializable()]
    public class Answer
    {
        /// <summary>
        /// Answer identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The answer's text
        /// </summary>
        public string Text { get; set; }
    }
}
