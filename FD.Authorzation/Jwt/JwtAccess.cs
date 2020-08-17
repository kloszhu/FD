using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using FD.Tool;
using Microsoft.AspNetCore.Http;
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

        public string SetToken(ClaimModel userinfo) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(GlobSectury.Secret);
            var authTime = DateTime.Now;//授权时间
            var expiresAt = authTime.AddDays(30);//过期时间
            SecurityTokenDescriptor tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(JwtClaimTypes.Audience,GlobSectury.ValidAudience),
                        new Claim(JwtClaimTypes.Issuer, GlobSectury.ValidIssuer),
                        new Claim(JwtClaimTypes.Name, userinfo.UserName),
                        new Claim(JwtClaimTypes.Id, userinfo.UserCode),      
                        new Claim("DepartMent", userinfo.DepartMent),
                        new Claim("IsOnUse", userinfo.IsOnUse.ToString()),
                        new Claim("Phone", userinfo.Phone),
                        new Claim("UserName",userinfo.UserName ),
                        new Claim("UserCode",userinfo.UserCode ),

                    }),
                Expires = expiresAt,
                //对称秘钥SymmetricSecurityKey
                //签名证书(秘钥，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token1 = tokenHandler.CreateToken(tokenDescripor);
            return tokenHandler.WriteToken(token1);


        }

        public ClaimModel GetToken(ClaimsPrincipal claimsPrincipal) {

            return new ClaimModel
            {
                DepartMent = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "DepartMent")?.Value,
                IsOnUse = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "IsOnUse").Value.StringConverter(),
                Phone = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "Phone")?.Value,
                UserCode = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "UserCode")?.Value,
                UserName = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == "UserName")?.Value,
            };
        }

        public ClaimModel GetToken(string token) {
            var data=  new JwtSecurityTokenHandler().ReadJwtToken(token);
            ClaimModel claim = new ClaimModel
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
