using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreProject.Model
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Code")]
        public string DepartmentCode { get; set; }

        [BsonElement("Name")]
        public string DepartmentName { get; set; }

        [BsonElement("CreateDate")]
        public DateTime CreateDate { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

    }
}
