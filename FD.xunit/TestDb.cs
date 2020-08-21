using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FD.DI;
using FD.DapperExtention;
namespace FD.xunit
{
    public class TestDb
    {
        [Fact]
        public void DBTest() {
            IOC.Start().AddDapper().End();
           var service=  IOC.GetService<IDapperRepository>();
            service.DbAccess.buffered = true;
            service.DbAccess.commandTimeout = 5;
            var p= service.DbAccess.Query("SELECT * FROM B_case");
           var d = service.DbAccess.Query("SELECT * FROM B_case");
        }
    }
}
