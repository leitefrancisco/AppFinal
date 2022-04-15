using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB
{
    internal class DataSource
    {
        private static DataSource instance = new DataSource();

        private MongoClient client;
        private IMongoDatabase db;

        private DataSource()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://atgp-mongodb:podepagurizada123@atgp-mongodb.xl10x.mongodb.net/ATGPmongodb?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            this.client = new MongoClient(settings);
            this.db = this.client.GetDatabase("ATGPmongodb");
        }

        public bool CheckConnection()
        {
            try
            {
                this.db.RunCommandAsync((Command<BsonDocument>)"{ping: 1}").Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /**
         * Finds all documents in collection
         */
        public List<BsonDocument> FindAll(string collection)
        {
            return this.db.GetCollection<BsonDocument>(collection).Find(new BsonDocument()).ToList();
        }

        public List<BsonDocument> FindMany(string collection, FilterDefinition<BsonDocument> filter)
        {
            return this.db.GetCollection<BsonDocument>(collection).Find(filter).ToList();
        }

        public List<BsonDocument> FindMany(string collection, FilterDefinition<BsonDocument> filter, int skip, int limit)
        {
            return this.db.GetCollection<BsonDocument>(collection).Find(filter).Skip(skip).Limit(limit).ToList();
        }

        public List<BsonDocument> FindAll(string collection, int skip, int limit)
        {
            return this.db.GetCollection<BsonDocument>(collection).Find(new BsonDocument()).Skip(skip).Limit(limit)
                .ToList();
        }

        /**
         * Finds one document in collection using a filter definition
         */
        public BsonDocument FindOne(string collection, FilterDefinition<BsonDocument> filter)
        {
            return this.db.GetCollection<BsonDocument>(collection).Find(filter).FirstOrDefault();
        }

        public bool InsertOne(string collection, BsonDocument document)
        {
            try
            {
                this.db.GetCollection<BsonDocument>(collection).InsertOne(document);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool DeleteOne(string collection, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                this.db.GetCollection<BsonDocument>(collection).DeleteOne(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateOne(string collection, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            try
            {
                this.db.GetCollection<BsonDocument>(collection).UpdateOne(filter, update);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static DataSource GetInstance()
        {
            return instance;
        }

        public void DisposeConnection()
        {
            this.db.Client.Cluster.Dispose();
        }
    }
}
