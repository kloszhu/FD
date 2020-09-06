using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FD.Mongo;
using FD.DI;
using FD.HttpManager;

namespace FD.xunit
{
    public class mongoTest
    {

        public IHttpClientHelper httpClient { get; set; }
        public IMongoManager mongoManager { get; set; }
        public mongoTest() {
            IOC.Start().AddMongo().AddHttpHelper().End();
             httpClient = IOC.GetService<IHttpClientHelper>();
             mongoManager = IOC.GetService<IMongoManager>();
        }
        [Fact]
        public void JsonAdd() {
            string json = "{name:\"周二结论\",id:2,data:[{name:\"周二结论\",id:1},{name:\"周二结论\",id:2},{name:\"周二结论\",id:3}]}";

            mongoManager.Add(json);
        }

        [Fact]

        public void SaveStock() {
            GetAllData();
        }

        public void GetAllData() {
           
           
            
            for (int i = 0; i < 300; i++)
            {
                var json = StockUrl(i);
                var d = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                mongoManager.Add(json);
            }

        }
        [Fact]
        public void GetMongo() {
            mongoManager.Find();
        }

        private string StockUrl(int page=1) {
            
            List<StringBuilder> stringBuilders = new List<StringBuilder>();
            string url= $"http://91.push2.eastmoney.com/api/qt/clist/get?cb=stock&pn={page}&pz=20&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:0+t:6,m:0+t:13,m:0+t:80,m:1+t:2,m:1+t:23&fields=f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f12,f13,f14,f15,f16,f17,f18,f20,f21,f23,f24,f25,f22,f11,f62,f128,f136,f115,f152&_=1599392900293";
            var data = httpClient.Get(url).Content.ReadAsStringAsync().Result;
            
            return data.Replace("stock(", "").Replace(");", "");
        }
    }
}
