using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FD.Authorzation.Jwt;
using FD.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FD.Tool;
namespace FD.Vue.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //      Login: '/auth/login',
        //Logout: '/auth/logout',
        //ForgePassword: '/auth/forge-password',
        //Register: '/auth/register',
        //twoStepCode: '/auth/2step-code',
        //SendSms: '/account/sms',
        //SendSmsErr: '/account/sms_err',
        //// get my info
        //UserInfo: '/user/info',
        //UserMenu: '/user/nav'
        [HttpPost]
        public IActionResult login([FromBody] LoginViewModel loginView)
        {
            JwtAccess jwt = new JwtAccess();
            string token = jwt.SetToken(new LoginModel { UserCode = loginView.username, UserName = loginView.username, DepartMent = loginView.password, IsOnUse = true, Phone = "18806446088" });
            return Ok(new { Token = token , RoleId = "admin" }.ToAntResult());
        }
        [HttpPost]
        public IActionResult logout() {
            JwtAccess jwt = new JwtAccess();
            jwt.logout();
            return Ok("注销成功1".ToResult(message: "注销成功"));
        }
        [HttpPost]
        public IActionResult forgetpassword() {
            return Ok();
        }
        [HttpPost]
        public IActionResult register() {
            return Ok();
        }

        [HttpPost]

        public IActionResult twoStepCode() {
            return Ok();
        }




    }
}
