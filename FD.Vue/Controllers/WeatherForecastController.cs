using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FD.Authorzation.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FD.Vue.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var p = HttpContext.User;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]

        public string Token([FromForm] LoginModel models)
        {
            JwtAccess jwt = new JwtAccess();
            models.IsOnUse = true;
            return jwt.SetToken(models);

        }


        [HttpPost]
        [Route("login")]
        [Obsolete]
        public IActionResult Login([FromBody] LoginInput input)
        {
            //从数据库验证用户名，密码 
            //验证通过 否则 返回Unauthorized

            //创建claim
            var authClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,input.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("password",input.Password),
                new Claim("projectid","我的项目")
            };
            IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret));

            var token = new JwtSecurityToken(

                   issuer: GlobSectury.ValidIssuer,
                   audience: GlobSectury.ValidAudience,
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );

            //返回token和过期时间
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

    }
}
