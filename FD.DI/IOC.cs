using Autofac;
using System;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace FD.DI
{
    /// <summary>
    /// 例如：IOC.Start().RegisterCommonInstance<TestService, ITestService>() .End();\n IOC.GetService<ITestService>();
    /// </summary>
    public static class IOC
    {
        public static IContainer container { get; set; }
        public static ContainerBuilder builder { get; set; }
        /// <summary>
        /// 实例化Autofac容器
        /// </summary>
        /// <typeparam name="InterFace"></typeparam>
        /// <returns></returns>
        public static InterFace GetService<InterFace>(){
            return container.Resolve<InterFace>();
        }

        public static IContainer End(this ContainerBuilder builders)
        {
            container= builders.Build();
            return container;
        }

        public static ContainerBuilder Start() {

            builder = new ContainerBuilder();
            return builder;
        }

      

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="AssemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(String AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }

        //将Services中的服务填充到Autofac中


    }


    public static class IOCExtention {


   


        public static ContainerBuilder Populate(this ContainerBuilder builder, IServiceCollection services)
        {
            builder.Populate(services);
            //新模块组件注册
            return builder;
        }
        /// <summary>
        /// 单实例单类注入
        /// </summary>
        /// <param name="type"></param>
        public static ContainerBuilder RegisterSingleInstance(this ContainerBuilder builders, Type type)
        {
            builders.RegisterType(type).AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
            return builders;
        }


        public static ContainerBuilder RegisterCommonInstance<Implementation, Interface>(this ContainerBuilder builders)
        {
            builders.RegisterType<Implementation>().As<Interface>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).InstancePerLifetimeScope();
            return builders;
        }


        public static ContainerBuilder RegisterStatic<Implementation, Interface>(this ContainerBuilder builders)
        {
            builders.RegisterType<Implementation>().As<Interface>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
            return builders;
        }


        /// <summary>
        /// 泛型注入
        /// </summary>
        /// <typeparam name="Implementation"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="Implementation">例如：Mongo<></param>
        /// <param name="Interface">例如: IMongo<></param>
        public static ContainerBuilder RegisterGenericInstance(this ContainerBuilder builders, Type Implementation, Type Interface)
        {
            builders.RegisterGeneric(Implementation).As(Interface).PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).InstancePerLifetimeScope();
            return builders;
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="AssemblyName"></param>
        /// <param name="predicate"></param>
        public static ContainerBuilder RegisterAssembly(this ContainerBuilder builders, string AssemblyName, Func<Type, bool> predicate = null)
        {
            if (predicate == null)
            {
                builders.RegisterAssemblyTypes(IOC.GetAssemblyByName(AssemblyName))
                               .AsSelf()
                               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                               .InstancePerLifetimeScope();
            }
            else
            {
                builders.RegisterAssemblyTypes(IOC.GetAssemblyByName(AssemblyName))
           .Where(predicate)
               .AsSelf()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
               .InstancePerLifetimeScope();
            }
            return builders;
        }
    }
}
