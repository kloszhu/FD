using Autofac;
using FD.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.AutoEntity
{
    public static class AutoEntityRegister
    {
        public static ContainerBuilder AddAutoEntity(this ContainerBuilder buider)
        {
            buider.RegisterStatic<AutoEntityManager,IAutoEntityManager>();
            return buider;
        }
    }
}
