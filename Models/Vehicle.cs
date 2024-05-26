using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pejecoins.Models
{
    public class Vehicle
    {
        [BsonId] // For mapping with the integrated MongoDB ID
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("registrationDetails")]
        public RegistrationDetailsClass RegistrationDetails { get; set; }

        [BsonElement("vehicle")]
        public VehicleClass Vehicles { get; set; }

        public class RegistrationDetailsClass
        {
            [BsonElement("registrationID")]
            public string RegistrationID { get; set; }

            [BsonElement("curp")]
            public string Curp { get; set; }

            [BsonElement("federalEntity")]
            public string FederalEntity { get; set; }

            [BsonElement("registrationYear")]
            public int RegistrationYear { get; set; }

            [BsonElement("owner")]
            public string Owner { get; set; }
        }

        public class VehicleClass
        {
            [BsonElement("type")]
            public string Type { get; set; }

            [BsonElement("brand")]
            public string Brand { get; set; }

            [BsonElement("model")]
            public string Model { get; set; }

            [BsonElement("year")]
            public int Year { get; set; }
        }
    }
}
