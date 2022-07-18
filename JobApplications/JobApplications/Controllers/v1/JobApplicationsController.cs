using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JobApplications.Engine.Interfaces;
using JobApplications.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace JobApplications.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly ILogger<JobApplicationsController> _logger;

        private readonly IJobApplicationProcessor _jobApplicationProcessor;

        /// <summary>
        /// Initializes a new instance of <see cref="JobApplicationsController"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jobApplicationProcessor"></param>
        public JobApplicationsController(ILogger<JobApplicationsController> logger, IJobApplicationProcessor jobApplicationProcessor)
        {
            _logger = logger;
            _jobApplicationProcessor = jobApplicationProcessor;
        }

        /// <summary>
        /// Endpoint that provides access to the functionality for processing job applications
        /// </summary>
        /// <returns>A list of qualified job applications or an empty array if none</returns>
        /// <example>{"job":[{"Id":"1","Question":"Do you have a car?","Answer":"yes"},{"Id":"2","Question":"Are you legally authorized to work in the USA?","Answer":"yes"}],"jobApplications":[{"Name":"John Smith","Questions":[{"Id":"1","Answer":"yes"},{"Id":"2","Answer":"yes"}]},{"Name":"John Doe","Questions":[{"Id":"1","Answer":"no"},{"Id":"2","Answer":"yes"}]},{"Name":"Marie Smith","Questions":[{"Id":"1","Answer":"No"},{"Id":"2","Answer":"No"}]},{"Name":"Paz Jensen","Questions":[{"Id":"1","Answer":"yes"},{"Id":"2","Answer":"yes"}]}]}</example>
        [HttpPost]
        [Route("~/api/v1/[controller]/ValidateJobApplications")]
        [SwaggerOperation(description:"Takes a job information and a list of job applications and validates which applications should be accepted")]
        public IActionResult ValidateJobApplications(
            [FromBody, SwaggerRequestBody("{\"job\":[{\"Id\":\"1\",\"Question\":\"Do you have a car?\",\"Answer\":\"yes\"},{\"Id\":\"2\",\"Question\":\"Are you legally authorized to work in the USA?\",\"Answer\":\"yes\"}],\"jobApplications\":[{\"Name\":\"John Smith\",\"Questions\":[{\"Id\":\"1\",\"Answer\":\"yes\"},{\"Id\":\"2\",\"Answer\":\"yes\"}]},{\"Name\":\"John Doe\",\"Questions\":[{\"Id\":\"1\",\"Answer\":\"no\"},{\"Id\":\"2\",\"Answer\":\"yes\"}]},{\"Name\":\"Marie Smith\",\"Questions\":[{\"Id\":\"1\",\"Answer\":\"No\"},{\"Id\":\"2\",\"Answer\":\"No\"}]},{\"Name\":\"Paz Jensen\",\"Questions\":[{\"Id\":\"1\",\"Answer\":\"yes\"},{\"Id\":\"2\",\"Answer\":\"yes\"}]}]}")] 
            JObject data)
        {
            var job = data.GetValue("job")?.ToString();
            var jobApplications = data.GetValue("jobApplications")?.ToString();
            if (!string.IsNullOrEmpty(job) && !string.IsNullOrEmpty(jobApplications))
            {
                // first lets try to get a list of jobs from the passed in jobsList
                var jobInfo = _jobApplicationProcessor.GetJobFromJsonInput(job);
                if (jobInfo != null)
                {
                    var jobApps = _jobApplicationProcessor.GetJobApplicationsFromJsonInput(jobInfo, jobApplications);
                    if (jobApps.Any())
                    {
                        var results = _jobApplicationProcessor.ValidateJobApplications(jobApps);
                        if (results == null) return Problem("There was an error evaluating the job applications");
                        return new JsonResult(results);
                    }

                    return Problem("There was an error trying to read the job applications");
                }

                return Problem("There was an error trying to read the qualifications");

            }
            return new JsonResult("No valid job applications");
        }
    }
}
