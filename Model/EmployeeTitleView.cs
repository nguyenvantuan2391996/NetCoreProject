using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace NetCoreProject.Model
{
    public class EmployeeTitleView
    {
        [BsonElement("DepartmentCode")]
        public string DepartmentCode { get; set; }

        [BsonElement("DepartmentName")]
        public string DepartmentName { get; set; }

        [BsonElement("JobTitleCode")]
        public string JobTitleCode { get; set; }

        [BsonElement("JobTitleName")]
        public string JobTitleName { get; set; }
    }
}
