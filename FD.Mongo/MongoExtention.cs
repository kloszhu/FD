using MongoDB.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Mongo
{
    public static class MongoExtention
    {
        public static string BsonConvert(this BsonDocument bsons) {
          return  Newtonsoft.Json.JsonConvert.SerializeObject(bsons,new BsonObjectIdConverter());
        }

        //public static BsonDocument BsonConvert(this string json) { 
        //    return Newtonsoft.Json.JsonConverter
        //}

    }
}
