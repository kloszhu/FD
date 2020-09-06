//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FD.Tool
//{
//    class MongoHelper
//    {
//        /// <summary>
//        /// 递归获取类
//        /// </summary>
//        public static Dictionary<string, object> Convert(this BsonDocuemnt bson) {


        
//                List<BsonElement> bsons = document.Elements.ToList();
//                ModelMap curModel;
//                objs.Add(curModel = new ModelMap(name));
//                foreach (BsonElement b in bsons)
//                {
//                    Debug.WriteLine(b.Name);

//                    if (curModel.PropNames == null)
//                    {
//                        curModel.PropTypes = new List<string>();
//                        curModel.PropNames = new List<string>();
//                    }

//                    if (b.Value.IsBsonDocument)
//                    {
//                        curModel.PropTypes.Add(b.Name);
//                        curModel.PropNames.Add(b.Name);

//                        GetDoc(b.Name, b.Value.ToBsonDocument(), objs);
//                    }
//                    else if (b.Value.IsBsonArray)
//                    { // List判断
//                        BsonArray arr = b.Value as BsonArray;
//                        BsonValue val = arr.FirstOrDefault();

//                        if (val.IsBsonDocument)
//                        {
//                            curModel.PropTypes.Add($"List<{b.Name}>");
//                            curModel.PropNames.Add(b.Name);

//                            GetDoc(b.Name, val.ToBsonDocument(), objs);
//                        }
//                        else
//                        {
//                            curModel.PropTypes.Add($"List<{val.BsonType.ToString()}>");
//                            curModel.PropNames.Add(b.Name);
//                        }

//                    }
//                    else
//                    {
//                        curModel.PropTypes.Add(b.Value.BsonType.ToString());
//                        curModel.PropNames.Add(b.Name);
//                    }
//                }
//            }
//        }
//    }
//}
