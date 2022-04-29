using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFinal.Models;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to gameMatch collection
    /// </summary>
    public class GameMatchDbAccess : MainDbAbstract<GameMatch>
    {
        /// <summary>
        /// Instantiate a new gameMatch access
        /// </summary>
        public GameMatchDbAccess()
        {
            this.CollectionName = "gameMatch";
        }

        /// <summary>
        /// Get all game matches for a given user
        /// </summary>
        /// <param name="userId">id of user to find game matches of</param>
        /// <returns>LinkedList of game matches from user</returns>
        public async Task<LinkedList<GameMatch>> GetUserGameMatches(string userId)
        {
            LinkedList<GameMatch> gms = new LinkedList<GameMatch>();
            var filter = "{\"userIds\": \"" + userId + "\"}";
            var gmsBson = await Db.FindMany(this.CollectionName, filter);
            foreach (var bgm in gmsBson)
            {
                gms.AddLast(GetObjectFromBsonDocument(bgm));
            }
            return gms;
        }

        protected override GameMatch GetObjectFromBsonDocument(BsonDocument document)
        {
            try
            {
                var id = document["_id"].ToString();
                var gameId = document["gameId"].AsString;
                var date = document["date"].AsString;
                string winnerId = null;
                if (document.Contains("mediaUrl"))
                {
                    winnerId = document["winnerId"].AsString;
                }

                LinkedList<string> userIds = new LinkedList<string>();
                foreach (var userId in document["userIds"].AsBsonArray)
                {
                    userIds.AddLast(userId.AsString);
                }

                return new GameMatch(id, gameId, date, userIds, winnerId);
            }
            catch (Exception)
            {
                return null;
            }

        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(GameMatch obj)
        {
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("gameId", obj.gameId);
            update = update.Set("date", obj.date);
            update = update.Set("winnerId", obj.winnerId);

            BsonArray friendsArray = new BsonArray();
            foreach (var id in obj.userIds)
            {
                friendsArray.Add(id);
            }

            update = update.Set("userIds", friendsArray);
            
            return update;
        }

        protected override BsonDocument GetBsonDocument(GameMatch obj)
        {
            return obj.GetBsonDocument();
        }
    }
}