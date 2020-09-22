using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.Mongo
{
    public static class MongoEntentions
    {
        /// <summary>
        /// 递归获取类
        /// </summary>
        public static void GetDoc(this List<MongoModelMap> objs,string name, BsonDocument document,int index=0)
        {
            index++;
            List<BsonElement> bsons = document.Elements.ToList();
            MongoModelMap curModel = new MongoModelMap(name);
            curModel.index= index;
            objs.Add(curModel);
            foreach (BsonElement b in bsons)
            {


                if (curModel.PropNames == null)
                {
                    curModel.PropTypes = new List<string>();
                    curModel.PropNames = new List<string>();
                }

                if (b.Value.IsBsonDocument)
                {
                    curModel.PropTypes.Add(b.Name);
                    curModel.PropNames.Add(b.Name);
                    objs.GetDoc(b.Name, b.Value.ToBsonDocument(), index);
                }
                else if (b.Value.IsBsonArray)
                { // List判断
                    BsonArray arr = b.Value as BsonArray;
                    BsonValue val = arr.FirstOrDefault();

                    if (val.IsBsonDocument)
                    {
                        curModel.PropTypes.Add($"List<{b.Name}>");
                        curModel.PropNames.Add(b.Name);

                        objs.GetDoc(b.Name, val.ToBsonDocument(), index);
                    }
                    else
                    {
                        curModel.PropTypes.Add($"List<{val.BsonType.ToString()}>");
                        curModel.PropNames.Add(b.Name);
                    }

                }
                else
                {
                    curModel.PropTypes.Add(b.Value.BsonType.ToString());
                    curModel.PropNames.Add(b.Name);
                }
            }
        }
    }
}
