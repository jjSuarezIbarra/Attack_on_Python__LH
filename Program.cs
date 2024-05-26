using MongoDB.Bson;
using MongoDB.Driver;
using Pejecoins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pejecoins
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            var user = "emilianogardav";
            var pswd = "emigymdav6122";
            var connectionUri = "mongodb+srv://" + user + ":" + pswd + "@mongolh.esxklru.mongodb.net/?retryWrites=true&w=majority&appName=MongoLH"; // Setting connection to MongoDB

            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings); // Create a new client and connect to the server

            try // Send a ping to confirm a successful connection
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var database = client.GetDatabase("DataLanes");
            var incidentCollection = database.GetCollection<Incident>("Incident");
            var vehicleCollection = database.GetCollection<Vehicle>("Vehicle");

            Incident newIncident = new Incident()
            {
                // Asignar valores a las propiedades
                IncidentID = "yr33th3t4",
                RegistrationID = "MLH 360",
                Date = new DateTime(2024, 11, 06, 0, 0, 0, DateTimeKind.Utc), // ISODate("2022-03-21T00:00:00Z")
                Speed = 90,
                Location = new Incident.LocationClass
                {
                    Latitude = 62.432608,
                    Longitude = -86.133209
                },
                Description = "Cruce en semáforo"
            };
            PutDocument(database, "Incident", newIncident);

            string curpValue = "TAAA020518HNLPVNA7";
            var vehicleFilter = Builders<Vehicle>.Filter.Eq("registrationDetails.curp", curpValue);
            List<Vehicle> vehiclesByCurp = GetDocuments(vehicleCollection, vehicleFilter);
            List<string> registrationNumbers = vehiclesByCurp.Select(v => v.RegistrationDetails.RegistrationID).ToList();

            Console.WriteLine("CURP : " + curpValue);
            Console.WriteLine("Cantidad de Vehiculos por CURP : " + vehiclesByCurp.Count);
            Console.WriteLine("IDs Unicos para CURP (" + registrationNumbers.Count + ") : " + registrationNumbers.ToList());

            var incidentFilter = Builders<Incident>.Filter.In("RegistrationID", registrationNumbers);
            List<Incident> incidentsByCurp = GetDocuments(incidentCollection, incidentFilter);
            Console.WriteLine("Incidentes para CURP (" + incidentsByCurp.Count + ") : " + incidentsByCurp);


            SharedData.IncidentsByCurp = incidentsByCurp;
            // var filter = Builders<Incident>.Filter.Eq("Id", ObjectId.Parse("665293679a43af9c52dc51c1");
            // var document = incidentCollection.Find(filter).First();
            //Console.Write(document);
            //Console.WriteLine("Presiona cualquier tecla para salir...");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

        }
        public static List<T> GetCollection<T>(IMongoCollection<T> collection)
        {
            return collection.Find(_ => true).ToList(); // Using lambda expression for getting a complete list of the Collection
        }
        public static List<T> GetDocuments<T>(IMongoCollection<T> collection, FilterDefinition<T> filter)
        {
            return collection.Find(filter).ToList(); // Using lambda expression for getting a complete list of the Collection
        }

        public static void PostDocument<T>(IMongoDatabase db, string collectionName, T document)
        {
            var collection = db.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }

        public static void PutDocument<T>(IMongoDatabase db, string collectionName, T newDocument)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", BsonValue.Create(newDocument.GetType().GetProperty("Id").GetValue(newDocument)));
            collection.ReplaceOne(filter, newDocument);
        }

        public static void DeleteDocument<T>(IMongoDatabase db, string collectionName, T documentToDelete)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", BsonValue.Create(documentToDelete.GetType().GetProperty("Id").GetValue(documentToDelete)));
            collection.DeleteOne(filter);
        }
        public static class SharedData
        {
            public static List<Incident> IncidentsByCurp { get; set; }
        }
    }
}
