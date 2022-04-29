﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFinal.Models;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppFinal.DB.AccessClasses
{
    /// <summary>
    /// Public access to messages collection
    /// </summary>
    public class MessageDbAccess : MainDbAbstract<Message>
    {
        /// <summary>
        /// Instantiate a new messages access
        /// </summary>
        public MessageDbAccess()
        {
            this.CollectionName = "messages";
        }

        /// <summary>
        /// Get messages sent and received by a user
        /// </summary>
        /// <param name="userId">id of user collection</param>
        /// <returns>LinkedList of all messages that have been sent and received</returns>
        public async Task<LinkedList<Message>> GetUserMessages(string userId)
        {
            var filter = "{$or: {\"by\": \"" + userId + "\", \"to\": \"" + userId + "\"}";
            var messagesBsonDocument = await Db.FindMany(this.CollectionName, filter);

            return GetLinkedListFromBsonList(messagesBsonDocument);
        }

        /// <summary>
        /// Get messages sent by a user
        /// </summary>
        /// <param name="userId">id of user collection</param>
        /// <returns>LinkedList of all messages that have been sent</returns>
        public async Task<LinkedList<Message>> GetUserSentMessages(string userId)
        {
            var filter = "{\"by\": \"" + userId + "\"}";
            var messagesBsonDocument = await Db.FindMany(this.CollectionName, filter);

            return GetLinkedListFromBsonList(messagesBsonDocument);
        }

        /// <summary>
        /// Get messages received by a user
        /// </summary>
        /// <param name="userId">id of user document</param>
        /// <returns>LinkedList of all messages that have been received</returns>
        public async Task<LinkedList<Message>> GetUserReceivedMessages(string userId)
        {
            var filter = "{\"to\": \"" + userId + "\"}";
            var messagesBsonDocument = await Db.FindMany(this.CollectionName, filter);

            return GetLinkedListFromBsonList(messagesBsonDocument);
        }

        /// <summary>
        /// Get unread messages received by a user
        /// </summary>
        /// <param name="userId">d of user document</param>
        /// <returns>LinkedList of all unread messages that have been received or are to be received</returns>
        public async Task<LinkedList<Message>> GetUserUnreadMessages(string userId)
        {
            var filter = "{\"to\": \"" + userId + "\", $or: {\"messageStatus\": \"" + MessageStatusGetter.GetMessageStatus(MessageStatus.SENT) + "\", \"messageStatus\": \"" + MessageStatusGetter.GetMessageStatus(MessageStatus.RECEIVED) + "\"}}";
            var messagesBsonDocument = await Db.FindMany(this.CollectionName, filter);

            return GetLinkedListFromBsonList(messagesBsonDocument);
        }

        protected override Message GetObjectFromBsonDocument(BsonDocument document)
        {
            try
            {
                var id = document["_id"].ToString();
                var sender = document["by"].AsString;
                var receiver = document["to"].AsString;
                var content = document["content"].AsString;
                string mediaUrl = null;
                if (document.Contains("mediaUrl"))
                {
                    mediaUrl = document["mediaUrl"].AsString;
                }

                var date = document["date"].AsString;
                var status = document["messageStatus"].AsString;

                return new Message(id, sender, receiver, content, mediaUrl, date, status);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        protected override UpdateDefinition<BsonDocument> GetUpdateDefinition(Message message)
        {

            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("by", message.sender);
            update = update.Set("to", message.receiver);
            update = update.Set("content", message.content);
            update = update.Set("mediaUrl", message.mediaUrl);
            update = update.Set("date", message.date);
            update = update.Set("messageStatus", MessageStatusGetter.GetMessageStatus(message.status));

            return update;
        }

        protected override BsonDocument GetBsonDocument(Message obj)
        {
            return obj.GetBsonDocument();
        }
    }
}