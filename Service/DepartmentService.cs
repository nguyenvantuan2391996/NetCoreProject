using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NetCoreProject.Model;

namespace NetCoreProject.Service
{
    public class DepartmentService
    {
        private readonly IMongoCollection<Department> _departments;

        // Connect mongodb
        public DepartmentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MyDemo"));
            var database = client.GetDatabase("MyDemo");

            _departments = database.GetCollection<Department>("Department");
        }

        // Method get all department
        public List<Department> GetAll() =>
            _departments.Find(department => true).ToList();

        // Method get by list department code
        public List<Department> GetByCodes(List<string> deptCodes)
        {
            var query = Builders<Department>.Filter.In(department => department.DepartmentCode, deptCodes);

            var listDepartment = _departments.Find(query).ToList();

            return listDepartment;
        }

        // Method get by department code
        public Department GetByOne(string deptCode) =>
            _departments.Find<Department>(department => department.DepartmentCode == deptCode).FirstOrDefault();

        // Method create department
        public Department Create(Department department)
        {
            _departments.InsertOne(department);
            return department;
        }

        // Method update department
        public void Update(Department departmentUpdate) =>
            _departments.ReplaceOne(department => department.DepartmentCode == departmentUpdate.DepartmentCode, departmentUpdate);

        // Method delete department
        public void Remove(string id) =>
            _departments.DeleteOne(department => department.Id == id);
    }
}
