using FD.Authorzation.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FD.Authorzition.Jwt
{
    public static  class JwtConfig
    {
        public static IServiceCollection AddJwt(this IServiceCollection services) {

            //services.AddAuthentication(options =>
            //{
            //    //认证middleware配置
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = GlobSectury.ValidateIssuerSigningKey,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret)),

            //        ValidateIssuer = GlobSectury.ValidateIssuer,
            //        ValidIssuer = GlobSectury.ValidIssuer,

            //        ValidateAudience = GlobSectury.ValidateAudience,
            //        ValidAudience = GlobSectury.ValidAudience,

            //        ValidateLifetime = GlobSectury.ValidateLifetime,

            //        ClockSkew = GlobSectury.ClockSkew
            //    };
            //});

            //添加身份验证
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //jwt token参数设置
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    //Token颁发机构
                    ValidIssuer = GlobSectury.ValidIssuer,
                    //颁发给谁
                    ValidAudience = GlobSectury.ValidAudience,
                    //这里的key要进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret)),


                };
            });



            return services;
        }

    

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthorization();
            return app;
        }


    }
}
