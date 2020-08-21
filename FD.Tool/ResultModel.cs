using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Tool
{
    public enum ResultState
    {
        成功= 20000,
        非法Token= 50008,
        非法重复登录= 50012,
        Token失效= 50014,
        禁止访问=403,
        无权限=401,
        接口调用成功=200,
        其他
    }
    public class ResultModel
    {
        public ResultState? code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public class AntResultModel 
    {
        public ResultState? code { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }

    public static class  ObjectResultExtention{ 
        public static ResultModel ToResult(this object obj,ResultState? code=null,string message=null)
        {
            return new ResultModel { code = code??ResultState.成功, message = message??"OK", data = obj };
        }

        public static AntResultModel ToAntResult(this object obj, ResultState? code = null, string message = null)
        {
            return new AntResultModel { code = code ?? ResultState.接口调用成功, message = message ?? "OK",  result = obj };
        }

    }
}
