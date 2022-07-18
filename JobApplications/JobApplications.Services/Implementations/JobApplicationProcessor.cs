using JobApplications.Engine.Interfaces;
using JobApplications.Models;
using JobApplications.Models.JsonImport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApplications.Engine.Implementations
{
    /// <inheritdoc/>
    public class JobApplicationProcessor : IJobApplicationProcessor
    {
        /// <inheritdoc/>
        public Job GetJobFromJsonInput(string jobInfoFromJson)
        {
            if (string.IsNullOrEmpty(jobInfoFromJson)) return null;
            try
            {
                var counter = 0;
                var qualifications = 
                    JsonConvert.DeserializeObject<IEnumerable<ListOfQuestionsWithAnAcceptableAnswerFromJson>>(jobInfoFromJson)?.ToList();
                if (qualifications == null ||
                    !qualifications.Any(x => !string.IsNullOrEmpty(x.Answer) && !string.IsNullOrEmpty(x.Question)))
                    return null;

                var jobInfo = new Job()
                {
                    Id = Guid.NewGuid(), // no job info is provided in the input string so just assign random values
                    Name = string.Empty, // no job name provided, leave blank
                    Qualifications = new Dictionary<Question, List<Answer>>(qualifications.Count)
                };
                foreach (var qualification in qualifications)
                {
                    var question = new Question()
                    {
                        Id = qualification.Id,
                        Text = qualification.Question
                    };

                    var validAnswers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Id = counter++, // specified input format doesn't have an answer id so just assign the next integer
                            Text = qualification.Answer
                        }
                    };
                    if (jobInfo.Qualifications.Keys.Any(x => x.Id == question.Id)) continue;
                    jobInfo.Qualifications.Add(question, validAnswers);
                }
                return jobInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public List<JobApplication> GetJobApplicationsFromJsonInput(Job job, string jobApplicationsFromJson)
        {
            if (job == null || string.IsNullOrEmpty(jobApplicationsFromJson)) return null;
            try
            {
                var jobApplicationsJson =
                    JsonConvert.DeserializeObject<List<JobApplicationsFromJson>>(jobApplicationsFromJson)?.ToList();
                if (jobApplicationsJson == null || !jobApplicationsJson.Any()) return null;

                return (from jobApplicationFromJson in jobApplicationsJson
                    where jobApplicationFromJson.Questions != null && jobApplicationFromJson.Questions.Any()
                    select new JobApplication()
                    {
                        Job = job,
                        Applicant = new Applicant() {Name = jobApplicationFromJson.Name},
                        AnsweredQuestions =
                            jobApplicationFromJson.Questions.ToDictionary(x => x.Id, x => new Answer() {Text = x.Answer})
                    }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public List<JobApplicationsFromJson> ValidateJobApplications(List<JobApplication> jobApplications)
        {
            if (jobApplications == null || !jobApplications.Any()) return null;
            try
            {
                var results = new List<JobApplicationsFromJson>();
                foreach (var jobApplication in jobApplications)
                {
                    var isValidApplication = true;
                    foreach (var qualification in jobApplication.Job.Qualifications)
                    {
                        var question = qualification.Key;
                        var validAnswers = qualification.Value;

                        if (jobApplication.AnsweredQuestions.TryGetValue(question.Id, out Answer answerProvidedByApplicant))
                        {
                            if (!validAnswers.Any(x => x.Text.Equals(answerProvidedByApplicant.Text,
                                    StringComparison.OrdinalIgnoreCase)))
                            {
                                isValidApplication = false;
                                break;
                            }
                        }
                        else
                        {
                            // question not answered
                            isValidApplication = false;
                            break;
                        }

                    }

                    if (!isValidApplication) continue;

                    results.Add(new JobApplicationsFromJson()
                    {
                        Name = jobApplication.Applicant.Name,
                        Questions = jobApplication.AnsweredQuestions.Select(x => new QuestionsInJobApplicationFromJson()
                            {Id = x.Key, Answer = x.Value.Text})
                    });
                }

                return results;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
