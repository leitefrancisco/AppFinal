using System;
using System.Collections.Generic;
using AppFinal.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to users collection
    /// </summary>
    public class UserDbAccess : MainDbAbstract<User>
    {
        /// <summary>
        /// Instantiate a new users access
        /// </summary>
        public UserDbAccess()
        {
            this.CollectionName = "users";
        }

        /// <summary>
        /// Get all friends of a given user
        /// </summary>
        /// <param name="userId">id of the user to find friends of</param>
        /// <returns>LinkedList with all friends of a given user</returns>
        public LinkedList<User> GetUserFriends(string userId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("friends", userId);
            var friendsBsonDocument = Db.FindMany(this.CollectionName, filter);

            return GetLinkedListFromBsonList(friendsBsonDocument);
        }

        protected override User GetObjectFromBsonDocument(BsonDocument document)
        {

            try
            {
                var id = document["_id"].ToString();
                var username = document["username"].AsString;
                var pictureUrl = document["profilePicture"].AsString;
                var email = document["email"].AsString;
                var language = document["language"].AsString;
                var region = document["region"].AsString;
                var accountLevel = document["accountLevel"].AsInt32;
                var achievementPoints = document["totalAchievementPoints"].AsInt32;

                var friends = new LinkedList<string>();
                foreach (var friend in document["friends"].AsBsonArray)
                {
                    friends.AddLast(friend.AsString);
                }

                var achievements = new LinkedList<string>();
                foreach (var achievement in document["achievements"].AsBsonArray)
                {
                    achievements.AddLast(achievement.AsString);
                }

                var friendsRequest = new LinkedList<string>();
                if (document.Contains("friendsRequest"))
                {
                    foreach (var req in document["friendsRequest"].AsBsonArray)
                    {
                        friendsRequest.AddLast(req.AsString);
                    }
                }

                return new User(id, username, pictureUrl, email, language, region, accountLevel, achievementPoints,
                    friends, achievements, friendsRequest);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(User user)
        {
            BsonArray friendsArray = new BsonArray();
            foreach (var friend in user.friends)
            {
                friendsArray.Add(friend);
            }

            BsonArray achievementsArray = new BsonArray();
            foreach (var achievement in user.achievements)
            {
                achievementsArray.Add(achievement);
            }

            BsonArray requestsArray = new BsonArray();
            foreach (var req in user.friendsRequest)
            {
                requestsArray.Add(req);
            }

            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("username", user.username)
                .Set("profilePicture", user.pictureUrl)
                .Set("email", user.email)
                .Set("language", user.language)
                .Set("region", user.region)
                .Set("accountLevel", user.accountLevel)
                .Set("totalAchievementPoints", user.achievementPoints)
                .Set("friends", friendsArray)
                .Set("achievements", achievementsArray)
                .Set("friendsRequest", requestsArray);

            return update;
        }

        protected override BsonDocument GetBsonDocument(User obj)
        {
            return obj.GetBsonDocument();
        }
    }
}