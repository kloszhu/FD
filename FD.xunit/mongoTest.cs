using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FD.Mongo;
using FD.DI;
using FD.HttpManager;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using FD.AutoEntity;
using FD.AutoEntity.Model;
using System.Data.SqlTypes;

namespace FD.xunit
{
    public class mongoTest
    {

        public IHttpClientHelper httpClient { get; set; }
        public IMongoManager mongoManager { get; set; }
        public mongoTest()
        {
            IOC.Start().AddMongo().AddHttpHelper().End();
            httpClient = IOC.GetService<IHttpClientHelper>();
            mongoManager = IOC.GetService<IMongoManager>();
        }
        [Fact]
        public void JsonAdd()
        {
            string json = "{name:\"周二结论\",id:2,data:[{name:\"周二结论\",id:1},{name:\"周二结论\",id:2},{name:\"周二结论\",id:3}]}";

            mongoManager.Add(json);
        }

        [Fact]

        public void SaveStock()
        {
            GetAllData();
        }

        public void GetAllData()
        {



            for (int i = 0; i < 300; i++)
            {
                var json = StockUrl(i);
                var d = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                mongoManager.Add(json);
            }

        }
        [Fact]
        public void GetMongo()
        {
            var p = mongoManager.Find();
        }

        private string StockUrl(int page = 1)
        {

            List<StringBuilder> stringBuilders = new List<StringBuilder>();
            string url = $"http://91.push2.eastmoney.com/api/qt/clist/get?cb=stock&pn={page}&pz=20&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:0+t:6,m:0+t:13,m:0+t:80,m:1+t:2,m:1+t:23&fields=f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f12,f13,f14,f15,f16,f17,f18,f20,f21,f23,f24,f25,f22,f11,f62,f128,f136,f115,f152&_=1599392900293";
            var data = httpClient.Get(url).Content.ReadAsStringAsync().Result;

            return data.Replace("stock(", "").Replace(");", "");
        }

        [Fact]
        public void TestMethod1()
        {
            IOC.Start().AddAutoEntity().AddMongo().End();
            var service = IOC.GetService<IAutoEntityManager>();
            var mongoManager = IOC.GetService<IMongoManager>();
            string conn = "mongodb://localhost:27017";
            string dbName = "stock";
            string collectName = "stocklist";

            MongoClient client = new MongoClient(conn);
            IMongoDatabase database = client.GetDatabase(dbName);
            IMongoCollection<BsonDocument> colls = database.GetCollection<BsonDocument>(collectName);

            BsonDocument document = colls
                .Find(Builders<BsonDocument>.Filter.Empty)
                //.SortByDescending(x => x["CreationTime"])
                .FirstOrDefault();
            // 递归 查询 BsonDocument
            List<MongoModelMap> list = new List<MongoModelMap>();

            //this.GetDoc("stocklist", document, list);
            list.GetDoc(collectName, document);

            var pppp = typeof(string);
      
            foreach (var item in list.OrderByDescending(a => a.index))
            {



                classDefineModel p = new classDefineModel
                {
                    assemblyName = "MongoDynamic",
                    className = item.Name,
                    moduleName = "Model",
                    typeAttributes = System.Reflection.TypeAttributes.Public,
                    // Praent = typeof(BaseModel<int>)
                };
                var fieldlist = new List<fieldDefineModel>();
                for (int i = 0; i < item.PropNames.Count; i++)
                {
                    fieldlist.Add(new fieldDefineModel
                    {
                        fieldAttribute = System.Reflection.FieldAttributes.Public,
                        propertyName = item.PropNames[i],
                        propertyType = FindType($"System.{item.PropTypes[i]}")
                    });
                }
                p.propertyDefineModels = fieldlist;
                service.Create(p);
                var t = service.AutoEntityList.Where(a => a.Name == "diff").FirstOrDefault();
                var ppd = fieldlist.GetType();
                var dpp = Type.GetType("System.Collections.Generic.List`1[[diff, MongoDynamic]]");
                var dpps = Type.GetType("MongoDynamic.diff", false,true);
            }


            var pp = service.AutoEntityList;
            //var student = pp.FirstOrDefault();
            //object po = Activator.CreateInstance(student);
            //po.ID = 2;
            //po.Name = "Student";
            //System.Reflection.PropertyInfo pi = po.GetType().GetProperty("Name");
            //pi.SetValue(po, "Name");
            //var tt = typeof(Newtonsoft.Json.JsonConvert);
            //MethodInfo method = tt.GetMethods().FirstOrDefault(a => a.Name == "SerializeObject");

            //var cc = method.Invoke(null, new[] { po });
            //var ii = Newtonsoft.Json.JsonConvert.SerializeObject(po);


        }

        private Type FindType(string name)
        {
            return Type.GetType(name, true, true);
        }



        /**/ /// <summary>
             ///  
             /// </summary>
             /// <param name="type"></param>
             /// <returns></returns>
        private Type ChangeToCSharpType(string type)
        {
            Type reval =typeof(string);
            switch (type.ToLower())
            {
                case "Int32":
                    reval = typeof(int?);
                    break;
                case "String":
                    reval = typeof(string);
                    break;
                case "Int64":
                    reval = typeof(Int64?);
                    break;
                case "Byte[]":
                    reval = typeof(byte[]);
                    break;
                case "Boolean":
                    reval = typeof(bool?);
                    break;
                case "DateTime":
                    reval = typeof(DateTime?);
                    break;
                case "Decimal":
                    reval = typeof(Decimal?);
                    break;
                case "Double":
                    reval = typeof(Double?);
                    break;
                case "Float":
                    reval = typeof(float?);
                    break;
                case "Single":
                    reval = typeof(Single?);
                    break;
                case "Int16":
                    reval = typeof(Int16?);
                    break;

                case "Byte":
                    reval = typeof(Byte?);
                    break;
                case "Guid":
                    reval = typeof(Guid?);
                    break;
                default:
                    break;
            }
            if (type.StartsWith("List<"))
            {
                
            }
            return reval;
        }


    }
}
