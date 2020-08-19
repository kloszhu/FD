using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Text;

namespace FD.Vue
{
    public static class Bootstraper
    {
        public static void UseVue8080(this IApplicationBuilder app) {
            string basePath = Directory.GetCurrentDirectory();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
@$"
NODE_ENV=production
VUE_APP_PREVIEW=false
VUE_APP_API_BASE_URL = 'http://localhost:5010/'
");
        
            File.WriteAllText(Path.Combine(basePath, "wwwroot", ".env"), stringBuilder.ToString());       
        }

    }
}
