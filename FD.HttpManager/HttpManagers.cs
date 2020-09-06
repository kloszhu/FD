using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FD.DI;

namespace FD.HttpManager
{
    public static class HttpManagers
    {
        public static ContainerBuilder AddHttpHelper(this ContainerBuilder buider)
        {
            buider.RegisterCommonInstance<HttpClientHelper, IHttpClientHelper>();
            return buider;
        }
    }
}
