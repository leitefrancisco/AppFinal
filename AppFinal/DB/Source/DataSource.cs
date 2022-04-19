using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.Source
{
    // TODO HIDE THE CLASS
    /// <summary>
    /// Data Source class
    /// </summary>
    public class DataSource
    {
        private static readonly DataSource Instance = new DataSource();

        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        /// <summary>
        /// Instantiate the data source
        /// </summary>
        private DataSource()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://atgp-mongodb:podepagurizada123@atgp-mongodb.xl10x.mongodb.net/ATGPmongodb?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            this._client = new MongoClient(settings);
            this._db = this._client.GetDatabase("ATGPmongodb");
        }

        /// <summary>
        /// Check connection with DB and shows collections
        /// </summary>
        /// <returns>success of connection</returns>
        public bool CheckConnection()
        {
            try
            {
                this._db.RunCommandAsync((Command<BsonDocument>)"{ping: 1}").Wait();
                ShowCollections();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Show collections in DB
        /// </summary>
        private void ShowCollections()
        {
            foreach (var collection in this._db.ListCollectionNames().ToList())
            {
                Console.WriteLine(collection);
            }
        }

        /// <summary>
        /// Find all documents in collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <returns>List of BsonDocuments</returns>
        public List<BsonDocument> FindAll(string collection)
        {
            return this._db.GetCollection<BsonDocument>(collection).Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Find all documents in collection that match the given filter
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <returns>List of BsonDocument that match the filter</returns>
        public List<BsonDocument> FindMany(string collection, FilterDefinition<BsonDocument> filter)
        {
            return this._db.GetCollection<BsonDocument>(collection).Find(filter).ToList();
        }

        /// <summary>
        /// Find the limit of documents after skipping some documents that match the given filter
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <param name="skip">number of documents to be skipped</param>
        /// <param name="limit">limit of documents to be retrieved</param>
        /// <returns>List of BsonDocument</returns>
        public List<BsonDocument> FindMany(string collection, FilterDefinition<BsonDocument> filter, int skip, int limit)
        {
            return this._db.GetCollection<BsonDocument>(collection).Find(filter).Skip(skip).Limit(limit).ToList();
        }

        /// <summary>
        /// Find all documents from a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="skip">number of documents to be skipped</param>
        /// <param name="limit">limit of documents to be retrieved</param>
        /// <returns>List of BsonDocument</returns>
        public List<BsonDocument> FindAll(string collection, int skip, int limit)
        {
            return this._db.GetCollection<BsonDocument>(collection).Find(new BsonDocument()).Skip(skip).Limit(limit)
                .ToList();
        }

        /// <summary>
        /// Finds one document in collection using a filter definition
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <returns>single BsonDocument</returns>
        public BsonDocument FindOne(string collection, FilterDefinition<BsonDocument> filter)
        {
            return this._db.GetCollection<BsonDocument>(collection).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Creates a new document in a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="document">BsonDocument to be added</param>
        /// <returns>success of insertion</returns>
        public bool InsertOne(string collection, BsonDocument document)
        {
            try
            {
                this._db.GetCollection<BsonDocument>(collection).InsertOne(document);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Deletes a document from a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">filter to check what document to be deleted</param>
        /// <returns>success of deletion</returns>
        public bool DeleteOne(string collection, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                this._db.GetCollection<BsonDocument>(collection).DeleteOne(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Updates a document in a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <param name="update">UpdateDefinition</param>
        /// <returns>success of update</returns>
        public bool UpdateOne(string collection, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            try
            {
                var res = this._db.GetCollection<BsonDocument>(collection).UpdateOne(filter, update);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Get Instance of DataSource
        /// </summary>
        /// <returns>Instance of DataSource</returns>
        public static DataSource GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Disposes of the current connection
        /// </summary>
        public void DisposeConnection()
        {
            this._db.Client.Cluster.Dispose();
        }
    }
}