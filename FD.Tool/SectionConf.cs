using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FD.Tool
{
    /// <summary>
    /// 读配置文件 例如：SectionConf.Config["RedisConfig:ReadWriteHosts"]
    /// </summary>
    public class SectionConf
    {
        private static IConfiguration config;

        /// <summary>
        /// 加载配置文件 例如：SectionConf.Config["RedisConfig:ReadWriteHosts"]
        /// </summary>
        public static IConfiguration Config
        {
            get
            {
                if (config != null) return config;
                config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
                return config;
            }
            set => config = value;
        }

    }
}
