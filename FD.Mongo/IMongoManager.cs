using MongoDB.Bson;
using MongoDB.Driver;

namespace FD.Mongo
{
    public interface IMongoManager
    {
        IMongoCollection<BsonDocument> Collection { get; set; }
        IMongoDatabase Database { get; set; }
        IMongoClient MongoClient { get; set; }

        void Add(string json);
        void init();
        void Find();
    }
}