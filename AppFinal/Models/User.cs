using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppFinal.Models
{
    internal class User
    {
        public string id { get; private set; }
        public string username { get; private set; }
        public string pictureUrl { get; private set; }
        public string email { get; private set; }
        public string language { get; private set; }
        public string region { get; private set; }
        public int accountLevel { get; private set; }
        public int achievementPoints { get; private set; }
        public LinkedList<string> friends { get; }
        public LinkedList<string> achievements { get; }

        public User(string id, string username, string pictureUrl, string email, string language, string region, int accountLevel, int achievementPoints, LinkedList<string> friends, LinkedList<string> achievements)
        {
            this.id = id;
            this.username = username;
            this.pictureUrl = pictureUrl;
            this.email = email;
            this.language = language;
            this.region = region;
            this.accountLevel = accountLevel;
            this.achievementPoints = achievementPoints;
            this.friends = friends;
            this.achievements = achievements;
        }

        public void UpdateRegion(string region)
        {
            this.region = region;
        }

        public void UpdateLanguage(string language)
        {
            this.language = language;
        }

        public void AddFriend(string userId)
        {
            this.friends.AddLast(userId);
        }

        public void RemoveFriend(string userId)
        {
            this.friends.Remove(userId);
        }

        private void AddAchievementPoints(int points)
        {
            this.achievementPoints += points;
        }

        private void AccountLevelUp()
        {
            this.accountLevel++;
        }

        public void NewAchievement(string achievementId)
        {
            this.achievements.AddLast(achievementId);
            // TODO add achievement points from db and check level 
        }
        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(username)}: {username}, {nameof(pictureUrl)}: {pictureUrl}, {nameof(email)}: {email}, {nameof(language)}: {language}, {nameof(region)}: {region}, {nameof(accountLevel)}: {accountLevel}, {nameof(achievementPoints)}: {achievementPoints}, {nameof(friends)}: {friends.Count}, {nameof(achievements)}: {achievements.Count}";
        }

        /**
         * Gets Bson Document with no id
         */
        public BsonDocument GetBsonDocument()
        {

            BsonArray friendsArray = new BsonArray();
            foreach (var friend in this.friends)
            {
                friendsArray.Add(friend);
            }

            BsonArray achievementsArray = new BsonArray();
            foreach (var achievement in this.achievements)
            {
                achievementsArray.Add(achievement);
            }

            var bsonDoc = new BsonDocument()
            {
                //{"_id", new ObjectId(this.id)},
                {"username", this.username},
                {"profilePicture", this.pictureUrl},
                {"email", this.email},
                {"language", this.language},
                {"region", this.region},
                {"accountLevel", this.accountLevel},
                {"totalAchievementPoints", this.achievementPoints},
                {"friends", friendsArray},
                {"achievements", achievementsArray}
            };

            return bsonDoc;
        }
    }
}
