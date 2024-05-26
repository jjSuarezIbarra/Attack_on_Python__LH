using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pejecoins.Models
{
    internal class Incident
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("incidentID")]
        public string IncidentID { get; set; }

        [BsonElement("registrationID")]
        public string RegistrationID { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("speed")]
        public int Speed { get; set; }

        [BsonElement("location")]
        public LocationClass Location { get; set; }

        public class LocationClass
        {
            [BsonElement("latitude")]
            public double Latitude { get; set; }

            [BsonElement("longitude")]
            public double Longitude { get; set; }
        }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
