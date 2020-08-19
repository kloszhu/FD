using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FD.Vue.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        public IActionResult sms() {
            return Ok();
        }

        [HttpPost]

        public IActionResult sms_err() {
            return Ok();
        }

    }
}
