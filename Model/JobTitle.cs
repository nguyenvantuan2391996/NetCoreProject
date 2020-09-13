using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreProject.Model
{
    public class JobTitle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Code")]
        public string JobTitleCode { get; set; }

        [BsonElement("Name")]
        public string JobTitleName { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
