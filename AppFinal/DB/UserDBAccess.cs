using System;
using System.Collections.Generic;
using System.Text;
using AppFinal.Models;
using MongoDB.Bson;
using MongoDB.Driver;


namespace AppFinal.DB
{
    internal class UserDBAccess : UserDBInterface
    {
        private static DataSource db = DataSource.GetInstance();
        private static readonly string collectionName = "users";

        public LinkedList<User> GetUsers()
        {
            var res = db.FindAll(collectionName);
            return GetLinkedListFromBsonList(res);
        }

        public LinkedList<User> GetUsers(Dictionary<string, string> filters)
        {

            var usersBsonDocument = db.FindMany(collectionName, GetFilterFromDictionary(filters));

            return GetLinkedListFromBsonList(usersBsonDocument);
        }

        public LinkedList<User> GetUserFriends(string userId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("friends", userId);
            var friendsBsonDocument = db.FindMany(collectionName, filter);

            return GetLinkedListFromBsonList(friendsBsonDocument);
        }

        public User GetUser(string userId)
        {
            var userBson = db.FindOne(collectionName, Builders<BsonDocument>.Filter.Eq("_id", userId));
            return GetUserFromBsonDocument(userBson);
        }

        public User GetUser(Dictionary<string, string> filters)
        {
            var userBson = db.FindOne(collectionName, GetFilterFromDictionary(filters));
            return GetUserFromBsonDocument(userBson);
        }

        public bool UpdateUser(User user)
        {
            string id = user.id;
            return db.UpdateOne(collectionName, GetFilterFromDictionary(new Dictionary<string, string>() { { "_id", id } }), GetUpdateDefinition(user));
        }

        public bool DeleteUser(string userId)
        {
            return db.DeleteOne(collectionName, GetFilterFromDictionary(new Dictionary<string, string>() { { "_id", userId } }));
        }

        public bool InsertUser(User user)
        {
            return db.InsertOne(collectionName, user.GetBsonDocument());
        }

        public LinkedList<User> GetLinkedListFromBsonList(List<BsonDocument> bsonList)
        {
            var users = new LinkedList<User>();
            foreach (var user in bsonList)
            {
                users.AddLast(GetUserFromBsonDocument(user));
            }

            return users;
        }

        public User GetUserFromBsonDocument(BsonDocument document)
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
                LinkedList<string> friends = new LinkedList<string>();
                foreach (var friend in document["friends"].AsBsonArray)
                {
                    friends.AddLast(friend.AsString);
                }
                LinkedList<string> achievements = new LinkedList<string>();
                foreach (var achievement in document["achievements"].AsBsonArray)
                {
                    achievements.AddLast(achievement.AsString);
                }

                return new User(id, username, pictureUrl, email, language, region, accountLevel, achievementPoints,
                    friends, achievements);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FilterDefinition<BsonDocument> GetFilterFromDictionary(Dictionary<string, string> filters)
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

        public UpdateDefinition<BsonDocument> GetUpdateDefinition(User user)
        {
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("username", user.username);
            update = update.Set("profilePicture", user.pictureUrl);
            update = update.Set("email", user.email);
            update = update.Set("language", user.language);
            update = update.Set("region", user.region);
            update = update.Set("accountLevel", user.accountLevel);
            update = update.Set("totalAchievementPoints", user.achievementPoints);
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
            update = update.Set("friends", friendsArray);
            update = update.Set("achievements", achievementsArray);
            Console.WriteLine(update.ToJson());
            return update;
        }
    }
}
