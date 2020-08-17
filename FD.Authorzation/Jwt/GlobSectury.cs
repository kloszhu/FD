using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using FD.Tool;

namespace FD.Authorzation.Jwt
{
    public static class GlobSectury
    {
        /// <summary>
        /// 是否受理验证
        /// </summary>
        public static bool ValidateIssuerSigningKey { get; set; } = SectionConf.Config["jwt:vilidIssuerKey"]==null? false:Convert.ToBoolean(SectionConf.Config["jwt:vilidIssuerKey"]);
        /// <summary>
        /// 秘钥，非常重要
        /// </summary>
        public static string Secret { get; set; } = SectionConf.Config["jwt:secret"] == null ? "314963663qqsdfdscomcreatebyzhu": SectionConf.Config["jwt:secret"];
        /// <summary>
        /// 是否颁发者验证
        /// </summary>
        public static bool ValidateIssuer { get; set; } = SectionConf.Config["jwt:ValidateIssuer"] == null ? false : Convert.ToBoolean(SectionConf.Config["jwt:ValidateIssuer"]);
        /// <summary>
        /// 受理者
        /// </summary>
        public static string ValidIssuer { get; set; } = SectionConf.Config["jwt:Issuer"] == null ? "http://localhost:5001" : SectionConf.Config["jwt:Issuer"]; 
        /// <summary>
        /// 是否验证颁发者
        /// </summary>
        public static bool ValidateAudience { get; set; } = SectionConf.Config["jwt:ValidateAudience"] == null ? false : Convert.ToBoolean(SectionConf.Config["jwt:ValidateAudience"]);
        /// <summary>
        /// 颁发者
        /// </summary>
        public static string ValidAudience = SectionConf.Config["jwt:Audience"] == null ? "http://localhost:5001" : SectionConf.Config["jwt:Audience"];
        /// <summary>
        /// 是否启用过期时间
        /// </summary>
        public static bool ValidateLifetime { get; set; } = SectionConf.Config["jwt:ValidateLifetime"] == null ? false : Convert.ToBoolean(SectionConf.Config["jwt:ValidateLifetime"]);
        /// <summary>
        /// 时钟偏移，多久过期
        /// </summary>
        public static TimeSpan ClockSkew = TimeSpan.FromMinutes(SectionConf.Config["jwt:ClockSkew"] == null ? 5 : Convert.ToInt32(SectionConf.Config["jwt:ClockSkew"]));
    }
}
