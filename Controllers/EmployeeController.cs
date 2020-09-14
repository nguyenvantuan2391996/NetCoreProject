using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Service;
using NetCoreProject.Model;
using System.Text.RegularExpressions;

namespace NetCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<EmployeeView>> GetAll() =>
            _employeeService.GetAll();

        [HttpGet("getbycodes")]
        public ActionResult<List<EmployeeView>> GetByCodes([FromBody]List<string> employeeCodes) =>
            _employeeService.GetByCodes(employeeCodes);

        [HttpGet("getbyone")]
        public ActionResult<Employee> GetByOne([FromQuery]string employeeCode)
        {
            var employee = _employeeService.GetByOne(employeeCode);

            // Checking record employee existed
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("getbydepartmentcodes")]
        public ActionResult<List<EmployeeView>> GetByDepartmentCodes([FromBody]List<string> departmentCodes) =>
            _employeeService.GetByDepartmentCodes(departmentCodes);

        [HttpGet("getbyjobtitlecodes")]
        public ActionResult<List<EmployeeView>> GetByJobTitleCodes([FromBody]List<string> jobtitleCodes) =>
            _employeeService.GetByJobTitleCodes(jobtitleCodes);

        [HttpPost]
        public ActionResult<List<EmployeeView>> Create([FromBody]Employee employee)
        {
            // Validate objectid
            if (Regex.IsMatch(employee.Id.ToString(), "^[0-9a-fA-F]{24}$"))
            {
                var empl = _employeeService.Create(employee);

                if (empl == null)
                {
                    return BadRequest();
                }

                return Ok(empl.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employeeUpdate)
        {
            // Validate objectid
            if (Regex.IsMatch(employeeUpdate.Id.ToString(), "^[0-9a-fA-F]{24}$"))
            {
                // Check exist record
                var job = _employeeService.GetByOne(employeeUpdate.Id);

                if (job == null)
                {
                    return BadRequest("JobTitle isn't existed");
                }

                _employeeService.Update(employeeUpdate);

                return Ok(employeeUpdate.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery]string employeeCode)
        {
            _employeeService.Delete(employeeCode);
            return Ok(employeeCode);
        }

    }
}