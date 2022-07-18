using System;
using System.Collections.Generic;
using System.Text;
using JobApplications.Models;
using JobApplications.Models.JsonImport;

namespace JobApplications.Engine.Interfaces
{
    /// <summary>
    /// Contract that defines operations available to job applications
    /// such as evaluation of qualified vs non qualified applications
    /// </summary>
    public interface IJobApplicationProcessor
    {
        /// <summary>
        /// Takes a json string and returns a job object
        /// </summary>
        /// <param name="jobInfoFromJson">Json string containing a job information</param>
        /// <returns>A <see cref="Job"/> instance</returns>
        Job GetJobFromJsonInput(string jobInfoFromJson);

        /// <summary>
        /// Takes a job object and a json string and returns a list of job applications
        /// </summary>
        /// <param name="job">Job information related with the job applications</param>
        /// <param name="jobApplicationsFromJson">Json string containing job applications</param>
        /// <returns>A list of <see cref="JobApplication"/></returns>
        List<JobApplication> GetJobApplicationsFromJsonInput(Job job, string jobApplicationsFromJson);

        /// <summary>
        /// Takes a list of job applications and based on the job's qualifications
        /// approves or discards applications
        /// </summary>
        /// <param name="jobApplications">The list to be processed.</param>
        /// <returns>A list of job applications that clear the qualifications requirements</returns>
        List<JobApplicationsFromJson> ValidateJobApplications(List<JobApplication> jobApplications);
    }
}
