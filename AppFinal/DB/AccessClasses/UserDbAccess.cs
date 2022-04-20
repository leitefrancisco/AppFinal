using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        /// User InsertOne(User user, string password) to create a new user
        /// </summary>
        [Obsolete("Don't use this method when creating a new user, use the method 'InsertOne(User user, string password)' to hash and salt the user's password", true)]
        public new bool InsertOne(User user)
        {
            return false;
        }

        /// <summary>
        /// Create a new user document
        /// </summary>
        /// <param name="user">user to be created</param>
        /// <param name="password">plain text password to be hashed and salted</param>
        /// <returns>success of creation</returns>
        public bool InsertOne(User user, string password)
        {
            var salt = GenerateRandomSalt();
            Console.WriteLine(Convert.ToBase64String(salt));

            var hashed = new Rfc2898DeriveBytes(password, salt).GetBytes(64);
            Console.WriteLine(Convert.ToBase64String(hashed));

            var bson = user.GetBsonDocument();
            bson.Add(new BsonElement("password", hashed));
            bson.Add(new BsonElement("salt", salt));

            return Db.InsertOne(this.CollectionName, bson);
        }

        /// <summary>
        /// Generate a random salt
        /// </summary>
        /// <returns>random salt as byte array</returns>
        /// <source>https://stackoverflow.com/questions/7272771/encrypting-the-password-using-salt-in-c-sharp</source>
        public static byte[] GenerateRandomSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[64];
            rng.GetBytes(bytes);
            return bytes;
        }

        /// <summary>
        /// Get password and salt from a user from the DB
        /// </summary>
        /// <param name="email">user email address</param>
        /// <returns>dictionary keys: salt, pass. Values: byte arrays</returns>
        public Dictionary<string, byte[]> GetHashedPasswordAndSalt(string email)
        {
            var dict = new Dictionary<string, byte[]>();
            var filter = Builders<BsonDocument>.Filter.Eq("email", email);
            var bson = Db.FindOne(this.CollectionName, filter);
            dict.Add("salt", bson["salt"].AsByteArray);
            dict.Add("pass", bson["password"].AsByteArray);

            return dict;
        }

        /// <summary>
        /// Check credentials against DB
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user plain text password</param>
        /// <returns>the user if log in is correct and null if it isn't</returns>
        public User Login(string email, string password)
        {
            var user = Db.FindOne(this.CollectionName, Builders<BsonDocument>.Filter.Eq("email", email));
            var passAndHash = GetHashedPasswordAndSalt(email);
            var checkHash = new Rfc2898DeriveBytes(password, passAndHash["salt"]).GetBytes(64);

            return Convert.ToBase64String(checkHash).Equals(Convert.ToBase64String(passAndHash["pass"])) ? GetObjectFromBsonDocument(user) : null;

        }

        /// <summary>
        /// Change a user's password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="email">user email address</param>
        /// <param name="oldPassword">current password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>invalid credentials: 0, update error: 1, success: 2</returns>
        public int UpdatePassword(string userId, string email, string oldPassword, string newPassword)
        {
            if (Login(email, oldPassword) == null) return 0;

            var salt = GenerateRandomSalt();
            var hashed = new Rfc2898DeriveBytes(newPassword, salt).GetBytes(64);

            var update = Builders<BsonDocument>.Update.Set("salt", salt).Set("password", hashed);

            return Db.UpdateOne(this.CollectionName, Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(userId)), update) ? 2 : 1;
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