using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using MongoDB.Bson;

namespace NetCoreProject.Model
{
    public class EmployeeView
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Code")]
        public string EmployeeViewCode { get; set; }

        [BsonElement("Name")]
        public string EmployeeViewName { get; set; }

        [BsonElement("Titles")]
        public List<EmployeeTitleView> Titles { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
