using Autofac;
using FD.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Mongo
{
    public static  class MongoRegister
    {
        public static ContainerBuilder AddMongo(this ContainerBuilder buider)
        {
            buider.RegisterCommonInstance<MongoManager, IMongoManager>();               
            return buider;
        }
    }
}
