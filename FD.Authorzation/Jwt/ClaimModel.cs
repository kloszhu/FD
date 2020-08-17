using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Authorzation.Jwt
{
    public class ClaimModel
    {
        public string UserName { get; set; } = "";
        public string UserCode { get; set; } = "";
        public string Phone { get; set; } = "";
        public string DepartMent { get; set; } = "";
        public bool IsOnUse { get; set; } = false;
    }
}
