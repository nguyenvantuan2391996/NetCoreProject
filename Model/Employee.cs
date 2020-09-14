using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace NetCoreProject.Model
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Code")]
        public string EmployeeCode { get; set; }

        [BsonElement("Name")]
        public string EmployeeName { get; set; }

        [BsonElement("Titles")]
        public List<EmployeeTitle> Titles { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
