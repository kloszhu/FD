using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FD.Authorzation.Jwt;
using FD.Authorzition.Jwt;
using FD.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VueCliMiddleware;

namespace FD.Vue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

  
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = GlobSectury.ValidateIssuer,
                    ValidateAudience = GlobSectury.ValidateAudience,
                    ValidAudience = GlobSectury.ValidAudience,
                    ValidIssuer = GlobSectury.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobSectury.Secret))
                };
            });

            //services.AddSingleton(typeof(IWebapiControllerActionService), typeof(WebapiControllerActionService));
            //services.AddSingleton(typeof(IActionDescriptorCollectionProvider), typeof(DefaultActionDescriptorCollectionProvider));
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            //跨域
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    //允许任何来源的主机访问
                    //TODO: 新的 CORS 中间件已经阻止允许任意 Origin，即设置 AllowAnyOrigin 也不会生效
                    //AllowAnyOrigin()
                    //设置允许访问的域
                    //TODO: 目前.NET Core 3.1 有 bug, 暂时通过 SetIsOriginAllowed 解决
                    //.WithOrigins(Configuration["CorsConfig:Origin"])
                    .SetIsOriginAllowed(t => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.AddFDSwagger();
            services.AddControllers();


    
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CorsMiddleware>();

            app.UseRouting();

            #region 这两个仔必须放这个中间，路由启动之后，端口映射之前

            app.UseAuthentication();
            app.UseAuthorization();
            #endregion
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            #region Swagger
            app.UseFDSwagger();
            #endregion

            #region 编译Vue
            app.UseVue8080();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "wwwroot";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve",9999);
                }

            });
            #endregion



        }
        
    }
}
