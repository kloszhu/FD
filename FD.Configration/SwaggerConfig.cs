using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FD.Configration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {

            return services;
        }

        public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {

            return app;
        }
    }
}
