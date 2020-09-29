using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FD.MVC.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace FD.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationPartManager _applicationPartManager;
        public HomeController(ILogger<HomeController> logger, ApplicationPartManager applicationPartManager)
        {
            _logger = logger;
            _applicationPartManager = applicationPartManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowFlow() {
            return View();
        }

        public IActionResult Privacy()
        {



            var controllerFeature = new ControllerFeature();
            _applicationPartManager.PopulateFeature(controllerFeature);
            var data = controllerFeature.Controllers.Select(x => new
           ControllerModel
            {
                Namespace = x.Namespace,
                FullController = x.FullName,
                ModuleName = x.Module.Name,
                ControllerName = x.Name,
                Actions = x.DeclaredMethods.Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute)))
                .Select(y => new ActionModel
                {
                    ActionName = y.Name,
                    ParameterCount = y.GetParameters().Length,
                    Parameters = y.GetParameters()
                      .Select(z => new ParampterModel
                      {
                          Name = z.Name,
                          FullName = z.ParameterType.FullName,
                          Position = z.Position
                          //Attrs = z.CustomAttributes.Select(m => new
                          //{
                          //    FullName = m.AttributeType.FullName,
                          //})
                      })
                }),
            });
            return View();
        }

        public class ControllerModel
        {
            public string ControllerName { get; set; }
            public IEnumerable<ActionModel> Actions { get; set; } = new List<ActionModel>();
            public string Namespace { get; internal set; }
            public string FullController { get; internal set; }
            public string ModuleName { get; internal set; }
        }
        public class ActionModel
        {
            public string ActionName { get; set; }
            public int? Flag { get; set; }
            public int ParameterCount { get; internal set; }
            public IEnumerable<ParampterModel> Parameters { get; set; } = new List<ParampterModel>();
        }
        public class ParampterModel
        {
            public string Name { get; internal set; }
            public string FullName { get; internal set; }
            public int Position { get; internal set; }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
