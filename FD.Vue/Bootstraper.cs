using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server.Features;


namespace FD.Vue
{
    public static class Bootstraper
    {
        public static void UseVue8080(this IApplicationBuilder app) {
            string basePath = Directory.GetCurrentDirectory();
            String ServerAddress = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            File.WriteAllText(Path.Combine(basePath, "clientapp","host.js"), $"var basepath='{ServerAddress??"http://localhost:5010"}'");       
        }

    }
}
