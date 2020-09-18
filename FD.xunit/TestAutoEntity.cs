using FD.AutoEntity;
using FD.AutoEntity.Model;
using FD.DI;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace FD.xunit
{
    public class TestAutoEntity
    {
        [Fact]
        public void Test1()
        {
            IOC.Start().AddAutoEntity().End();
            var service = IOC.GetService<IAutoEntityManager>();
            classDefineModel p = new classDefineModel
            {
                assemblyName = "ceshi",
                className = "student",
                moduleName = "ceshi",
                typeAttributes = System.Reflection.TypeAttributes.Public,
                Praent=typeof(BaseModel<int>)
            };
            var list = new List<fieldDefineModel>();
            list.Add(new fieldDefineModel { fieldAttribute = System.Reflection.FieldAttributes.Public, propertyName = "ID", propertyType = typeof(int) });
            list.Add(new fieldDefineModel { fieldAttribute = System.Reflection.FieldAttributes.Public, propertyName = "Code", propertyType = typeof(Guid?) });
            list.Add(new fieldDefineModel { fieldAttribute = System.Reflection.FieldAttributes.Public, propertyName = "Name", propertyType = typeof(string) });
            p.propertyDefineModels = list;
            service.Create(p);
            var pp = service.AutoEntityList;
            var student = pp.FirstOrDefault();
            object po = Activator.CreateInstance(student);
            //po.ID = 2;
            //po.Name = "Student";
            System.Reflection.PropertyInfo pi = po.GetType().GetProperty("Name");
            pi.SetValue(po, "Name");
            var tt = typeof(Newtonsoft.Json.JsonConvert);
            MethodInfo method = tt.GetMethods().FirstOrDefault(a => a.Name == "SerializeObject");
          
            var cc= method.Invoke(null, new[] { po });
            var ii = Newtonsoft.Json.JsonConvert.SerializeObject(po);

        }
    }
}
