using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreProject.Model
{
    public class EmployeeTitle
    {
        [BsonElement("DepartmentCode")]
        public string DepartmentCode { get; set; }

        [BsonElement("JobTitleCode")]
        public string JobTitleCode { get; set; }
    }
}
