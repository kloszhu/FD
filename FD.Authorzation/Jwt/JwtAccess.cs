using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using FD.Tool;
using IdentityModel;

namespace FD.Authorzation.Jwt
{
    public class JwtAccess
    {
        //public string SetToken(ClaimModel claim) {
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim("DepartMent",claim.DepartMent ),
        //         new Claim("IsOnUse",claim.IsOnUse.ToString() ),
        //          new Claim("Phone",claim.Phone ),
        //           new Claim("UserCode",claim.UserCode ),
        //            new Claim("UserName",claim.UserName ),
        //    };


        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret));


        //    //var jwtToken = new JwtSecurityToken(
        //    //header: new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256)),
        //    //payload: new JwtPayload(claims)

        //    //  );

        //    var token = new JwtSecurityToken(
        //        issuer: GlobSectury.ValidIssuer,
        //        audience: GlobSectury.ValidAudience,
        //        claims: claims,
        //        notBefore: DateTime.Now,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        public string logout() {
            var authClaims = new Claim[] { };
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret));

            var token = new JwtSecurityToken(

                   issuer: GlobSectury.ValidIssuer,
                   audience: GlobSectury.ValidAudience,
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string SetToken(LoginModel userinfo) {
            var authClaims = new Claim[] {
                        new Claim(JwtClaimTypes.Audience,GlobSectury.ValidAudience),
                        new Claim(JwtClaimTypes.Issuer, GlobSectury.ValidIssuer),
                        new Claim(JwtClaimTypes.Name, userinfo.UserName),
                        new Claim(JwtClaimTypes.Id, userinfo.UserCode),
                        new Claim("DepartMent", userinfo.DepartMent),
                        new Claim("IsOnUse", userinfo.IsOnUse.ToString()),
                        new Claim("Phone", userinfo.Phone),
                        new Claim("UserName",userinfo.UserName ),
                        new Claim("UserCode",userinfo.UserCode ),

                    };
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret));

            var token = new JwtSecurityToken(

                   issuer: GlobSectury.ValidIssuer,
                   audience: GlobSectury.ValidAudience,
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public LoginModel GetToken(ClaimsPrincipal claimsPrincipal) {

            return new LoginModel
            {
                DepartMent = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "DepartMent")?.Value,
                IsOnUse = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "IsOnUse").Value.StringConverter(),
                Phone = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "Phone")?.Value,
                UserCode = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "UserCode")?.Value,
                UserName = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "UserName")?.Value,
            };
        }

        public LoginModel GetToken(string token) {
            var data=  new JwtSecurityTokenHandler().ReadJwtToken(token);
            LoginModel claim = new LoginModel
            {
                DepartMent = data.Claims.FirstOrDefault(a => a.Type == "DepartMent").Value,
                IsOnUse = data.Claims.FirstOrDefault(a => a.Type == "IsOnUse").Value.StringConverter(),
                Phone = data.Claims.FirstOrDefault(a => a.Type == "Phone").Value,
                UserCode = data.Claims.FirstOrDefault(a => a.Type == "UserCode").Value,
                UserName = data.Claims.FirstOrDefault(a => a.Type == "UserName").Value,
            };
            return claim;
        }

    }
}
