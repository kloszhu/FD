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
            data.UserName = "����";
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
            ProxyGenerator generator = new ProxyGenerator();//ʵ��������������������  
            Interceptor interceptor = new Interceptor();//ʵ��������������  

            //ʹ�á�������������������Person���󣬶�����ʹ��new�ؼ�����ʵ����  
            TestInterceptor test = generator.CreateClassProxy<TestInterceptor>(interceptor);
            Console.WriteLine("��ǰ����:{0},������:{1}", test.GetType(), test.GetType().BaseType);
            Console.WriteLine();
            test.MethodInterceptor();
            Console.WriteLine();
            test.NoInterceptor();
            Console.WriteLine();
          
        }


    }
} 
