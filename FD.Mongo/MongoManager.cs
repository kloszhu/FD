using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using System.Linq;
namespace FD.Mongo
{
    public class MongoManager : IMongoManager
    {
        public IMongoClient MongoClient { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<BsonDocument> Collection { get; set; }
        public void init()
        {
            MongoClient = new MongoClient("mongodb://localhost:27018");
            Database = MongoClient.GetDatabase("stock");
            Collection = Database.GetCollection<BsonDocument>("stocklist");
        }
        public MongoManager() {
            init();
        }

        public void Add(string json)
        {
             MongoDB.Bson.BsonDocument document= MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);
            Collection.InsertOne(document);
        }

        public void Find()
        {
            BsonDocument elements = new BsonDocument();
            var builder = Builders<BsonDocument>.Filter.Exists(a=>a.Elements.First(b=>b.Name=="data").Value!=null);
            Collection.Find<BsonDocument>(builder);
        }
    }
}
