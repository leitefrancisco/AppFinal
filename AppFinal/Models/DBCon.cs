using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace AppFinal.Models
{
    public class DBCon
    {


        public ObjectId Id { get; set; }


        //public async Task<List<TodoItem>> GetAllItem()
        //{
        //    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://atgp-mongodb:<podepagurizada123>@atgp-mongodb.xl10x.mongodb.net/ATGPmongodb?retryWrites=true&w=majority");
        //    settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        //    var client = new MongoClient(settings);
        //    var database = client.GetDatabase("NOME DA DATABASE Q QUER PEGAR VAI AQUI");
        //}



    }
}
