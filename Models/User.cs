using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Webproj.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Email { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public string CreatedAt { get; set; }
    }
}