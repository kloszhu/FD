using FD.Authorzation.Jwt;
using System;
using Xunit;

namespace FD.xunit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            FD.Authorzation.Jwt.JwtAccess jwt = new Authorzation.Jwt.JwtAccess();
            var data = new LoginModel();
            data.UserName = "÷Ï–€";
            var d= jwt.SetToken(data);
            var p = jwt.GetToken(d);
           

        }
    }
}
