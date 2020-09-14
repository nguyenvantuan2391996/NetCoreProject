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

        public DepartmentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MyDemo"));
            var database = client.GetDatabase("MyDemo");

            _departments = database.GetCollection<Department>("Department");
        }

        public List<Department> GetAll() =>
            _departments.Find(department => true).ToList();

        public List<Department> GetByCodes(List<string> deptCodes)
        {
            var query = Builders<Department>.Filter.In(department => department.DepartmentCode, deptCodes);

            var listDepartment = _departments.Find(query).ToList();

            return listDepartment;
        }


        public Department GetByOne(string deptCode) =>
            _departments.Find<Department>(department => department.DepartmentCode == deptCode).FirstOrDefault();

        public Department Create(Department department)
        {
            _departments.InsertOne(department);
            return department;
        }

        public void Update(Department departmentIn) =>
            _departments.ReplaceOne(department => department.Id == departmentIn.Id, departmentIn);

        public void Remove(string id) =>
            _departments.DeleteOne(department => department.Id == id);
    }
}
