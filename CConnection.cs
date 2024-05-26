using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows.Forms;
using Pejecoins.Properties;

namespace Pejecoins
{
    internal class CConnection
    {
        static String server = "localhost";
        static String port = "27017";

        public static MongoClient client = new MongoClient("mongodb://" + server + ":" + port);


    }
}
