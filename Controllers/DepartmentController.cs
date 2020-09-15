using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Model;
using NetCoreProject.Service;
using System.Text.RegularExpressions;

namespace NetCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public ActionResult<List<Department>> GetAll() =>
            _departmentService.GetAll();

        [HttpGet("getbycodes")]
        public ActionResult<List<Department>> GetByCodes([FromBody]List<String> deptCodes)
        {
            var listDepartment = _departmentService.GetByCodes(deptCodes);

            // Checking database has record department 
            if (listDepartment.Count == 0)
            {
                return NotFound();
            }
            return Ok(listDepartment);
        }

        [HttpGet("getbyone")]
        public ActionResult<Department> GetByOne([FromQuery]string deptCode)
        {
            var department = _departmentService.GetByOne(deptCode);

            // Checking database has record department 
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public ActionResult Create([FromBody]Department department)
        {
            // Validate departmentCode
            if (_departmentService.GetByOne(department.DepartmentCode) == null)
            {
                var dept = _departmentService.Create(department);

                if (dept == null)
                {
                    return BadRequest();
                }

                return Ok(dept.Id);
            }
            else
            {
                return BadRequest("Department Code is existed");
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody]Department department)
        {
            // Check exist record
            var dept = _departmentService.GetByOne(department.DepartmentCode);

            if (dept == null)
            {
                return BadRequest("Department isn't existed");
            }

            department.Id = dept.Id;
            _departmentService.Update(department);

            return Ok(department.Id);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery]string deptCode)
        {
            _departmentService.Remove(deptCode);
            return Ok(deptCode);
        }

    }
}