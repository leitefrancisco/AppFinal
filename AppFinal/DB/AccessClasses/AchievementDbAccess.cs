using System;
using AppFinal.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to achievements collection
    /// </summary>
    public class AchievementDbAccess : MainDbAbstract<Achievement>
    {

        /// <summary>
        /// Instantiate a new achievements access
        /// </summary>
        public AchievementDbAccess()
        {
            this.CollectionName = "achievements";
        }

        /// <summary>
        /// Get an achievement by its name
        /// </summary>
        /// <param name="name">name of achievement</param>
        /// <returns>Achievement object</returns>
        public Achievement GetAchievementByName(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            return GetObjectFromBsonDocument(Db.FindOne(this.CollectionName, filter));

        }

        protected override Achievement GetObjectFromBsonDocument(BsonDocument document)
        {
            try
            {
                var postId = document["_id"].ToString();
                var name = document["name"].AsString;
                var thumbnailUrl = document["thumbnail"].AsString;
                var description = document["description"].AsString;
                var achievementPoints = document["achievementPoints"].AsInt32;

                return new Achievement(postId, name, thumbnailUrl, description, achievementPoints);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(Achievement obj)
        {
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("thumbnail", obj.thumbnailUrl);
            update = update.Set("description", obj.description);
            update = update.Set("achievementPoints", obj.achievementPoints);

            return update;
        }

        protected override BsonDocument GetBsonDocument(Achievement obj)
        {
            return obj.GetBsonDocument();
        }
    }
}