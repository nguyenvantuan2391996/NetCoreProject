using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Service;
using NetCoreProject.Model;
using System.Text.RegularExpressions;

namespace NetCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController : Controller
    {
        private readonly JobTitleService _jobTitleService;

        public JobTitleController(JobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        [HttpGet]
        public ActionResult<List<JobTitle>> GetAll() =>
            _jobTitleService.GetAll();

        [HttpGet("getbycodes")]
        public ActionResult<List<JobTitle>> GetByCodes ([FromBody]List<String> jobTitleCodes)
        {
            var listJob = _jobTitleService.GetByCodes(jobTitleCodes);

            // Checking database has record department 
            if (listJob.Count == 0)
            {
                return NotFound();
            }
            return Ok(listJob);
        }

        [HttpGet("getbyone")]
        public ActionResult<Department> GetByOne([FromQuery]string jobTitleCode)
        {
            var jobTitle = _jobTitleService.GetByOne(jobTitleCode);

            // Checking database has record department 
            if (jobTitle == null)
            {
                return NotFound();
            }

            return Ok(jobTitle);
        }

        [HttpPost]
        public ActionResult Create(JobTitle jobTitle)
        {
            // Validate objectid
            if (Regex.IsMatch(jobTitle.Id.ToString(), "^[0-9a-fA-F]{24}$"))
            {
                var job = _jobTitleService.Create(jobTitle);

                if (job == null)
                {
                    return BadRequest();
                }

                return Ok(job.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult Update(JobTitle jobTitleUpdate)
        {
            // Validate objectid
            if (Regex.IsMatch(jobTitleUpdate.Id.ToString(), "^[0-9a-fA-F]{24}$"))
            {
                // Check exist record
                var job = _jobTitleService.GetByOne(jobTitleUpdate.Id);

                if (job == null)
                {
                    return BadRequest("JobTitle isn't existed");
                }

                _jobTitleService.Update(jobTitleUpdate);

                return Ok(jobTitleUpdate.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery]string jobTitleCode)
        {
            _jobTitleService.Delete(jobTitleCode);
            return Ok(jobTitleCode);
        }

    }
}