namespace JobApplications.Models.JsonImport
{
    /// <summary>
    /// The only purpose of this class is to provide and object to serialize the list of questions
    /// coming from the endpoint, since I didn't want to modify the format of the input since didn't
    /// knew if it was allowed
    /// </summary>
    public class ListOfQuestionsWithAnAcceptableAnswerFromJson
    {
        /// <summary>
        /// Question identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Question text
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Answer text
        /// </summary>
        public string Answer { get; set; }

    }
}
