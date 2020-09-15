using FD.AutoEntity;
using FD.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FD.xunit
{
    public  class TestAutoEntity
    {
        [Fact]
        public void Test1()
        {
            IOC.Start().AddAutoEntity().End();
            var service = IOC.GetService<IAutoEntityManager>();
            FD.AutoEntity.AutoEntityManager.classDefineModel p = new AutoEntityManager.classDefineModel {
                assemblyName = "ceshi", className = "student", moduleName = "ceshi", typeAttributes = System.Reflection.TypeAttributes.Public,

            };
            var list = new List<AutoEntityManager.fieldDefineModel>();
            list.Add(new AutoEntityManager.fieldDefineModel { fieldAttribute= System.Reflection.FieldAttributes.Public, propertyName="ID", propertyType=typeof(int)  });
            list.Add(new AutoEntityManager.fieldDefineModel { fieldAttribute = System.Reflection.FieldAttributes.Public, propertyName = "Code", propertyType = typeof(Guid?) });
            list.Add(new AutoEntityManager.fieldDefineModel { fieldAttribute = System.Reflection.FieldAttributes.Public, propertyName = "Name", propertyType = typeof(string) });
            p.propertyDefineModels = list;
            service.Create(p);
            var pp = service.AutoEntityList;

            var student = pp.FirstOrDefault();
           object po=  Activator.CreateInstance(student);
            //po.ID = 2;
            //po.Name = "Student";
            System.Reflection.PropertyInfo pi = po.GetType().GetProperty("Name");
            pi.SetValue(po, "Name");

          var  ii=  Newtonsoft.Json.JsonConvert.SerializeObject(po);

        }
    }
}
