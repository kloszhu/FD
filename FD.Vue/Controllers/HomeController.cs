using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FD.Authorzation.Jwt;
using System.IdentityModel.Tokens.Jwt;

namespace FD.Vue.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : ControllerBase
    {

       
     

        [HttpGet]
        
        public IActionResult Test()
        {
            List<string> Items = new List<string>();
            //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEZXBhcnRNZW50IjoiIiwiSXNPblVzZSI6IkZhbHNlIiwiUGhvbmUiOiIiLCJVc2VyQ29kZSI6IiIsIlVzZXJOYW1lIjoi5pyx6ZuEIiwibmJmIjoxNTk3NjQzNDQzLCJleHAiOjE1OTc2NDcwNDMsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMSJ9.M_d7XNtB_04S_QgyhrIoLc1yc6eLPThazD2sM379raQ"
            System.Security.Claims.ClaimsPrincipal User = HttpContext.User;
            foreach (var item in User.Claims)
            {
                Items.Add(item.Type);
                Items.Add(item.Value);
            }
            return Ok(Items);
           
        }

        [HttpGet]

        public IActionResult Test1()
        {
            JwtAccess jwt = new JwtAccess();
    
            return Ok(jwt.GetToken(HttpContext.User));

        }


    }
}
