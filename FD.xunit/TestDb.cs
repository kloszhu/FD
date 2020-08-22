using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FD.DI;
using FD.DapperExtention;
using System.Linq;

namespace FD.xunit
{
    public class TestDb
    {
        [Fact]
        public void DBTest() {
            IOC.Start().AddDapper().End();
           var service=  IOC.GetService<IDapperRepository>();
            var schema = IOC.GetService<IDynamicRepository>();
            service.DbAccess.buffered = true;
            service.DbAccess.commandTimeout = 5;
            var data=  schema.GetSchema().ToList();
        }
    }
}
