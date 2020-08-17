using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FD.Authorzation.Jwt;
using FD.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FD.Tool;
namespace FD.Vue.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult login([FromBody]LoginViewModel  loginView) {
            JwtAccess jwt = new JwtAccess();
           string token= jwt.SetToken(new LoginModel { UserCode = loginView. username, UserName = loginView.username, DepartMent = loginView. password, IsOnUse = true, Phone = "18806446088" });
            return Ok(token.ToResult());
        }

        public IActionResult info()
        {

            return Ok(new
            {
                roles = new string[] { "admin" },
                introduction = "I am a super administrator",
                avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
                name = "Super Admin"
            }.ToResult());

        }
    }
}
