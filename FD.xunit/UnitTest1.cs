using Castle.DynamicProxy;
using FD.Authorzation.Jwt;
using FD.DI;
using System;
using Xunit;

namespace FD.xunit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            FD.Authorzation.Jwt.JwtAccess jwt = new Authorzation.Jwt.JwtAccess();
            var data = new LoginModel();
            data.UserName = "朱雄";
            var d= jwt.SetToken(data);
            var p = jwt.GetToken(d);
           

        }


        [Fact]
        public void TestService() {
         var p=    IOC.Start()
                .RegisterCommonInstance<TestService, ITestService>()
                .RegisterCommonInstance<TestService1, ITestService1>()
                .End();
            var service= IOC.GetService<ITestService>();
            service.SendMsg();
        }
        [Fact]
        static void TestMain()
        {
            ProxyGenerator generator = new ProxyGenerator();//实例化【代理类生成器】  
            Interceptor interceptor = new Interceptor();//实例化【拦截器】  

            //使用【代理类生成器】创建Person对象，而不是使用new关键字来实例化  
            TestInterceptor test = generator.CreateClassProxy<TestInterceptor>(interceptor);
            Console.WriteLine("当前类型:{0},父类型:{1}", test.GetType(), test.GetType().BaseType);
            Console.WriteLine();
            test.MethodInterceptor();
            Console.WriteLine();
            test.NoInterceptor();
            Console.WriteLine();
          
        }


    }
} 
