﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NetCoreProject.Model;

namespace NetCoreProject.Service
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;

        // Connect mongodb
        public EmployeeService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MyDemo"));
            var database = client.GetDatabase("MyDemo");

            _employees = database.GetCollection<Employee>("Employee");
        }

        // Method get all EmployeeView Join [Employee] with [Department || JobTitle]
        public List<EmployeeView> GetAll()
        {
            var listEmployeeView = _employees.Aggregate()
                .Lookup("Department", "Titles.DepartmentCode", "Code", "DepartmentTitles")
                .Lookup("JobTitle", "Titles.JobTitleCode", "Code", "JobTitles")
                .As<BsonDocument>()
                .ToList();

            // Convert data to return object EmployeeView
            return this.convertEmployeeView(listEmployeeView);
        }

        // Method get EmployeeView Join [Employee] with [Department || JobTitle] by list employee code
        public List<EmployeeView> GetByCodes(List<string> employeeCodes)
        {
            // Query filter
            var query = Builders<Employee>.Filter.In(employee => employee.EmployeeCode, employeeCodes);

            var listEmployeeView = _employees.Aggregate()
                .Match(query)
                .Lookup("Department", "Titles.DepartmentCode", "Code", "DepartmentTitles")
                .Lookup("JobTitle", "Titles.JobTitleCode", "Code", "JobTitles")
                .As<BsonDocument>()
                .ToList();

            return this.convertEmployeeView(listEmployeeView);
        }

        // Method get EmployeeView Join [Employee] with [Department || JobTitle] by list department code
        public List<EmployeeView> GetByDepartmentCodes(List<string> departmentCodes)
        {
            // Query filter
            var query = Builders<Employee>.Filter.In("Titles.DepartmentCode", departmentCodes);

            var listEmployeeView = _employees.Aggregate()
                .Match(query)
                .Lookup("Department", "Titles.DepartmentCode", "Code", "DepartmentTitles")
                .Lookup("JobTitle", "Titles.JobTitleCode", "Code", "JobTitles")
                .As<BsonDocument>()
                .ToList();

            return this.convertEmployeeView(listEmployeeView);
        }

        // Method get EmployeeView Join [Employee] with [Department || JobTitle] by list jobtitle code
        public List<EmployeeView> GetByJobTitleCodes(List<string> jobtitleCodes)
        {
            // Query filter
            var query = Builders<Employee>.Filter.In("Titles.JobTitleCode", jobtitleCodes);

            var listEmployeeView = _employees.Aggregate()
                .Match(query)
                .Lookup("Department", "Titles.DepartmentCode", "Code", "DepartmentTitles")
                .Lookup("JobTitle", "Titles.JobTitleCode", "Code", "JobTitles")
                .As<BsonDocument>()
                .ToList();

            return this.convertEmployeeView(listEmployeeView);
        }

        // Method get Employee by employee code
        public Employee GetByOne(string employeeCode) =>
            _employees.Find<Employee>(employee => employee.EmployeeCode == employeeCode).FirstOrDefault();


        // Method create Employee
        public Employee Create(Employee employee)
        {
            _employees.InsertOne(employee);
            return employee;
        }

        // Method update Employee
        public void Update(Employee employeeUpdate) =>
            _employees.ReplaceOne(employee => employee.Id == employeeUpdate.Id, employeeUpdate);

        // Method delete Employee
        public void Delete(string employeeCode) =>
            _employees.DeleteOne(employee => employee.EmployeeCode == employeeCode);

        // Method convert data object EmployeeView
        public List<EmployeeView> convertEmployeeView(List<BsonDocument> listEmployeeView)
        {
            // Convert data to return object EmployeeView
            List<EmployeeView> list = new List<EmployeeView>();

            foreach (var item in listEmployeeView)
            {
                EmployeeView empl = new EmployeeView();
                List<EmployeeTitleView> listTitleView = new List<EmployeeTitleView>();

                empl.Id = item[0].ToString();
                empl.EmployeeViewCode = item["Code"].ToString();
                empl.EmployeeViewName = item["Name"].ToString();

                // Get value DepartmentCode, DepartmentName
                foreach (var title in item["DepartmentTitles"].AsBsonArray)
                {
                    EmployeeTitleView emplTitleView = new EmployeeTitleView();

                    emplTitleView.DepartmentCode = title["Code"].ToString();
                    emplTitleView.DepartmentName = title["Name"].ToString();

                    listTitleView.Add(emplTitleView);
                }

                // Get value JobTitleCode, JobTitleName
                int index = 0;
                foreach (var title in item["JobTitles"].AsBsonArray)
                {
                    listTitleView[index].JobTitleCode = title["Code"].ToString();
                    listTitleView[index].JobTitleName = title["Name"].ToString();

                    index++;
                }

                empl.Titles = listTitleView;

                empl.Description = item["Description"].ToString();

                list.Add(empl);
            }

            return list;
        }
    }
}
