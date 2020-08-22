using System;
using System.Collections.Generic;
using System.Text;
using FD.DI;
using Autofac;

namespace FD.DapperExtention
{
    public static class StaticLoad
    {
        public static ContainerBuilder AddDapper(this ContainerBuilder buider) {
            buider.RegisterCommonInstance<DapperRepository, IDapperRepository>()
               .RegisterCommonInstance<DbAccess, IDbAccess>()
               .RegisterCommonInstance<DBProvider, IDbProvider>()
               .RegisterCommonInstance<DynamicProvider, IDynamicProvider>()
               .RegisterCommonInstance<DynamicRepository, IDynamicRepository>()
               .RegisterCommonInstance<FileShemaRepository, IFileShemaRepository>();
            return buider;
        }
    }
}
