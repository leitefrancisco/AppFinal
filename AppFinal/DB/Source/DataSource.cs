using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppFinal.DB.AccessClasses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AppFinal.DB.Source
{
    /// <summary>
    /// Data Source to be hashed and hidden
    /// </summary>
    public class DataSource
    {
        private static readonly DataSource Instance = new DataSource();

        private readonly HttpClient _httpClient;
        private readonly string path = "http://35.204.176.180:8080/";

        /// <summary>
        /// Instantiate the data source
        /// </summary>
        private DataSource()
        {
            this._httpClient = new HttpClient();
        }

        /// <summary>
        /// Check connection with DB and shows collections
        /// </summary>
        /// <returns>success of connection</returns>
        public bool CheckConnection()
        {
            return false;
            //try
            //{
            //    this._httpClient.RunCommandAsync((Command<BsonDocument>)"{ping: 1}").Wait();
            //    ShowCollections();
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// Show collections in DB
        /// </summary>
        private void ShowCollections()
        {
            //foreach (var collection in this._httpClient.ListCollectionNames().ToList())
            //{
            //    Console.WriteLine(collection);
            //}
        }

        /// <summary>
        /// Find all documents in collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <returns>List of BsonDocuments</returns>
        public async Task<List<BsonDocument>> FindAll(string collection)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.path + collection);
            //request.Content = new StringContent("{\"region\": \"Taubate\"}", Encoding.UTF8, "application/json");

            var responseMessage = await this._httpClient.SendAsync(request);

            var response = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(response);

            var array = BsonSerializer.Deserialize<BsonArray>(response);
            List<BsonDocument> documents = new List<BsonDocument>();
            foreach (var r in array)
            {
                documents.Add(r.AsBsonDocument);
            }
            return documents;
            
        }

        public async Task Test(string collection)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.path + collection);
            request.Content = new StringContent("{\"region\": \"Taubate\"}", Encoding.UTF8, "application/json");

            var responseMessage = await this._httpClient.SendAsync(request);

            var response = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(response);

            var array = BsonSerializer.Deserialize<BsonArray>(response);
            Console.WriteLine("Results");
            foreach (var r in array)
            {
                var user = new UserDbAccess().GetUserFromBson(r.AsBsonDocument);
                Console.WriteLine("User: " + user);
            }

        }

        /// <summary>
        /// Find all documents in collection that match the given filter
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <returns>List of BsonDocument that match the filter</returns>
        public async Task<List<BsonDocument>> FindMany(string collection, string filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.path + collection);
            request.Content = new StringContent(filter, Encoding.UTF8, "application/json");

            var responseMessage = await this._httpClient.SendAsync(request);

            var response = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(response);

            var array = BsonSerializer.Deserialize<BsonArray>(response);
            List<BsonDocument> documents = new List<BsonDocument>();
            foreach (var r in array)
            {
                documents.Add(r.AsBsonDocument);
            }
            return documents;
        }
        

        /// <summary>
        /// Finds one document in collection using a filter definition
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">FilterDefinition</param>
        /// <returns>single BsonDocument</returns>
        public async Task<BsonDocument> FindOne(string collection, string filter)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this.path + collection);
            request.Content = new StringContent(filter, Encoding.UTF8, "application/json");

            var responseMessage = await this._httpClient.SendAsync(request);

            var response = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(response);

            var obj = BsonSerializer.Deserialize<BsonArray>(response);
            return obj.ToList()[0].AsBsonDocument;
        }

        /// <summary>
        /// Creates a new document in a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="document">BsonDocument to be added</param>
        /// <returns>success of insertion</returns>
        public bool InsertOne(string collection, BsonDocument document)
        {
            return false;
            //try
            //{
            //    this._httpClient.GetCollection<BsonDocument>(collection).InsertOne(document);
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return false;
            //}
        }

        /// <summary>
        /// Deletes a document from a collection
        /// </summary>
        /// <param name="collection">collection name</param>
        /// <param name="filter">filter to check what document to be deleted</param>
        /// <returns>success of deletion</returns>
        public bool DeleteOne(string collection, FilterDefinition<BsonDocument> filter)
        {
            return false;
            //try
            //{
            //    this._httpClient.GetCollection<BsonDocument>(collection).DeleteOne(filter);
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return false;
            //}
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
            return false;
            //try
            //{
            //    var res = this._httpClient.GetCollection<BsonDocument>(collection).UpdateOne(filter, update);
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return false;
            //}
        }

        /// <summary>
        /// Get Instance of DataSource
        /// </summary>
        /// <returns>Instance of DataSource</returns>
        public static DataSource GetInstance()
        {
            return Instance;
        }

        ///// <summary>
        ///// Disposes of the current connection
        ///// </summary>
        //public void DisposeConnection()
        //{
        //    this._httpClient.Client.Cluster.Dispose();
        //}
    }
}