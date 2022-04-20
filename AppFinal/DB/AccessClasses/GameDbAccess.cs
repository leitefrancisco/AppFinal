using System;
using AppFinal.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to games collection
    /// </summary>
    public class GameDbAccess : MainDbAbstract<Game>
    {
        /// <summary>
        /// Instantiate a new games access
        /// </summary>
        public GameDbAccess()
        {
            this.CollectionName = "games";
        }

        protected override Game GetObjectFromBsonDocument(BsonDocument document)
        {
            try
            {
                var id = document["_id"].ToString();
                var name = document["name"].AsString;
                var thumbnailUrl = document["thumbnail"].AsString;
                var description = document["description"].AsString;
                var path = document["path"].AsString;

                return new Game(id, name, thumbnailUrl, description, path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(Game obj)
        {
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("name", obj.name);
            update = update.Set("thumbnail", obj.thumbnailUrl);
            update = update.Set("description", obj.description);
            update = update.Set("path", obj.path);

            return update;
        }

        protected override BsonDocument GetBsonDocument(Game obj)
        {
            return obj.GetBsonDocument();
        }
    }
}