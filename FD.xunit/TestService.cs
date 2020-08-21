using System;
using System.Collections.Generic;
using System.Text;

namespace FD.xunit
{
    public interface ITestService {
        public void SendMsg();
    }
    public class TestService : ITestService
    {
        public void SendMsg()
        {
            Console.WriteLine("PPPPPPP");
        }
    }


    public interface ITestService1
    {
        public void SendMsg();
    }
    public class TestService1 : ITestService1
    {
        public void SendMsg()
        {
            Console.WriteLine("PPPPPPP");
        }
    }
}
