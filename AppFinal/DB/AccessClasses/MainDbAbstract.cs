using System;
using System.Collections.Generic;
using AppFinal.DB.Source;
using AppFinal.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{

    /// <summary>
    /// Abstract class implementing Main DB connection interface
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public abstract class MainDbAbstract<T> : IMainDbInterface<T> where T : class
    {
        // Instance of DataSource to connect to db
        protected static readonly DataSource Db = DataSource.GetInstance();

        // Each access class for a collection has its own collection name
        protected string CollectionName { get; set; }

        public bool DeleteOne(string objId)
        {
            return Db.DeleteOne(this.CollectionName, Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objId)));
        }

        public LinkedList<T> FindMany()
        {
            var bsonList = Db.FindAll(this.CollectionName);
            return GetLinkedListFromBsonList(bsonList);
        }

        public LinkedList<T> FindMany(Dictionary<string, string> filters)
        {
            var objectsBsonDocument = Db.FindMany(this.CollectionName, GetFilterFromDictionary(filters));
            return GetLinkedListFromBsonList(objectsBsonDocument);
        }

        public T FindOne(string objId)
        {
            var objectBson = Db.FindOne(this.CollectionName, Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objId)));
            return GetObjectFromBsonDocument(objectBson);
        }

        public T FindOne(Dictionary<string, string> filters)
        {
            var objBson = Db.FindOne(this.CollectionName, GetFilterFromDictionary(filters));
            return GetObjectFromBsonDocument(objBson);
        }

        /// <summary>
        /// Get FilterDefinition from a Dictionary
        /// </summary>
        /// <param name="filters">Dictionary with field name as key and value to be checked as value</param>
        /// <returns>FilterDefinition with the values in the filters</returns>
        protected FilterDefinition<BsonDocument> GetFilterFromDictionary(Dictionary<string, string> filters)
        {
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Empty;
            foreach (var keyValuePair in filters)
            {
                Console.WriteLine(keyValuePair);
                var lineFilter = builder.Eq(keyValuePair.Key, keyValuePair.Value);
                filter &= lineFilter;
            }

            return filter;

        }

        /// <summary>
        /// Transform a BsonDocument List to a LinkedList of object
        /// </summary>
        /// <param name="bsonList">List of BsonDocument from DB</param>
        /// <returns>LinkedList of objects</returns>
        protected LinkedList<T> GetLinkedListFromBsonList(List<BsonDocument> bsonList)
        {
            var objects = new LinkedList<T>();
            foreach (var obj in bsonList)
            {
                objects.AddLast(GetObjectFromBsonDocument(obj));
            }

            return objects;
        }

        /// <summary>
        /// Transform a BsonDocument into an object of the given class
        /// </summary>
        /// <param name="document">BsonDocument</param>
        /// <returns>Object of the class</returns>
        protected abstract T GetObjectFromBsonDocument(BsonDocument document);

        /// <summary>
        /// Generate an UpdateDefinition from an object of a given class in order to update a document in the database
        /// </summary>
        /// <param name="obj">Object of a given class</param>
        /// <returns>UpdateDefinition</returns>
        protected abstract UpdateDefinition<BsonDocument> GetUpdateDefinition(T obj);

        public bool InsertOne(T obj)
        {
            return Db.InsertOne(this.CollectionName, GetBsonDocument(obj));
        }

        public bool UpdateOne(T obj, string objId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objId));
            return Db.UpdateOne(this.CollectionName, filter, GetUpdateDefinition(obj));
        }

        /// <summary>
        /// Get BsonDocument from an object of a given class
        /// </summary>
        /// <param name="obj">Object of a given class</param>
        /// <returns>BsonDocument of a given object</returns>
        protected abstract BsonDocument GetBsonDocument(T obj);
    }
}