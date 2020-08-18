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
            String ServerAddress = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
@$"
# just a flag
ENV = 'server'

# base api
VUE_APP_BASE_API = '{ServerAddress??"http://localhost:5000"}'
");
        
            File.WriteAllText(Path.Combine(basePath, "wwwroot", ".env.server"), stringBuilder.ToString());       
        }

    }
}
