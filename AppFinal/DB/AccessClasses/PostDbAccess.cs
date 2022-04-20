using System;
using System.Collections.Generic;
using AppFinal.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to posts collection
    /// </summary>
    public class PostDbAccess : MainDbAbstract<Post>
    {
        /// <summary>
        /// Instantiate a new posts access
        /// </summary>
        public PostDbAccess()
        {
            this.CollectionName = "posts";
        }

        protected override Post GetObjectFromBsonDocument(BsonDocument document)
        {
            try
            {
                var postId = document["_id"].ToString();
                var userId = document["userId"].AsString;
                var date = document["date"].AsString;
                var content = document["content"].AsString;
                string mediaUrl = null;
                if (document.Contains("mediaUrl"))
                {
                    mediaUrl = document["mediaUrl"].AsString;
                }

                var likes = new LinkedList<string>();
                foreach (var like in document["likes"].AsBsonArray)
                {
                    likes.AddLast(like.AsString);
                }

                var comments = new LinkedList<string>();
                foreach (var comment in document["comments"].AsBsonArray)
                {
                    comments.AddLast(comment.AsString);
                }

                return new Post(postId, userId, date, content, likes, mediaUrl, comments);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(Post post)
        {
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("content", post.Content);
            update = update.Set("media", post.MediaUrl);

            BsonArray likesArray = new BsonArray();
            foreach (var like in post.Likes)
            {
                likesArray.Add(like);
            }

            BsonArray commentsArray = new BsonArray();
            foreach (var comment in post.Comments)
            {
                commentsArray.Add(comment);
            }
            update = update.Set("likes", likesArray);
            update = update.Set("comments", commentsArray);

            return update;
        }

        protected override BsonDocument GetBsonDocument(Post obj)
        {
            return obj.GetBsonDocument();
        }
    }
}