using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace FD.Swagger
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddFDSwagger(this IServiceCollection services,string[] xmlFiles=null)
        {
            services.AddSwaggerGen(p =>
            {
                //设置Json路径中的文件夹和简介
                p.SwaggerDoc("v1", new OpenApiInfo()//这个属性可以有多个，他是用于配置一页swagger中页面的标题和下面的SwaggerEndpoint（但是这个属性添加多个占时感觉没卵用）
                {
                    Version = "v1",
                    Title = "FlowDesigner",
                    Description = "基于.NET Core 3.1 的流程管理器",
                    Contact = new OpenApiContact
                    {
                        Name = "klosszhu",
                        Email = "314963663@qq.com",
                        Url = new Uri("http://cnblogs.com/microfisher"),
                    },
                });
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = System.AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                p.IncludeXmlComments(xmlPath);
                if (xmlFiles != null)
                {
                    for (int i = 0; i < xmlFiles.Length; i++)
                    {
                        p.IncludeXmlComments(xmlFiles[i]);
                    }
                }

                #region 启用swagger验证功能
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称一致即可，CoreAPI。
                p.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                    new OpenApiSecurityScheme()
                    {
                        Reference=new OpenApiReference()
                        {
                            Id="CoreAPI",
                            Type=ReferenceType.SecurityScheme
                        }
                    },Array.Empty<string>()
                    }
                });
                p.AddSecurityDefinition("CoreAPI", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion

            });
        



            return services;
        }

        public static IApplicationBuilder UseFDSwagger(this IApplicationBuilder app) {
            app.UseSwagger();//添加Swagger
            app.UseSwaggerUI(a =>//添加SwaggerUI
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "流程管理器");//配置上的aggerUI和名称可以有多个
            });
            app.UseReDoc(c => {
                c.SpecUrl = " /swagger/v1/swagger.json";
            });
            return app;
        }
    }
}
