namespace JobApplications.Models.JsonImport
{
    /// <summary>
    /// The only purpose of this class is to provide and object to serialize the question/answer information
    /// coming from the endpoint, since I didn't want to modify the format of the input since didn't
    /// knew if it was allowed
    /// </summary>
    public class QuestionsInJobApplicationFromJson
    {
        /// <summary>
        /// Answer identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The answer's text
        /// </summary>
        public string Answer { get; set; }
    }
}
