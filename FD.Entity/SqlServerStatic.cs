using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entity
{
    public static class SqlServerStatic
    {
        public static void UseSqlLite(this IServiceCollection services)
        {

            services.AddDbContext<TestDbContext>(options => options.UseSqlite("db.db", a => a.MigrationsAssembly("FD.Entity")));
        }
    }
}
