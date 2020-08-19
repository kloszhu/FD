using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FD.Vue.Conventions
{
    public class ApiConvention : IApplicationModelConvention
    {
        private static readonly string basePath = Directory.GetCurrentDirectory();
        public void Apply(ApplicationModel application)
        {

            StringBuilder builder = new StringBuilder();
            List<MyRouteModel> myRoutes = new List<MyRouteModel>();
            foreach (var controller in application.Controllers)
            {


                if (controller.Attributes.Count(a => a.GetType().Equals(typeof(RouteAttribute))) > 0)
                {
                    var route = (RouteAttribute)controller.Attributes.FirstOrDefault(a => a.GetType().Equals(typeof(RouteAttribute)));
                    var regix = route.Template;
                    myRoutes.AddRange(controller.Actions.Where(ac => ac.Attributes.Count(a => a.GetType().BaseType.Equals(typeof(HttpMethodAttribute))) > 0)
                        .Select(p => new MyRouteModel
                        {
                            urlString = $"/{regix.Replace("[controller]", controller.ControllerName).Replace("[action]", p.ActionName)}",
                            method = ((HttpMethodAttribute)p.Attributes.FirstOrDefault(a => a.GetType().BaseType.Equals(typeof(HttpMethodAttribute)))).HttpMethods,
                            actionName = p.ActionName,
                            isParampter = p.Parameters.Count() > 0 ? true : false,
                            Parampters = p.Parameters.ToList(),
                            controllerName = controller.ControllerName
                        }).ToList());
                }
            }
            string filename = Path.Combine(basePath, "controller.txt");
            File.WriteAllText(filename, WriteJsFunction(myRoutes.Distinct().ToList()).ToString());
        }


        private StringBuilder WriteJsFunction(List<MyRouteModel> myRoutes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in myRoutes.Select(a => a.controllerName).Distinct())
            {
                builder.AppendLine($"export let " + item.ToLower() + " = {");
                myRoutes.Where(a=>a.controllerName==item).ToList().ForEach(p =>
                {

                    foreach (var methd in p.method)
                    {
                        string func = @"  " + methd.ToLower() + p.actionName.ToLower() + @": function(" + (p.isParampter ? "parameter" : "") + @")  
{
    return request({
    url: '" + p.urlString.ToLower() + @"',
      method: '" + methd.ToLower() + @"',
      " + (p.isParampter ? "data: parameter " : "") + @"
    })
  },";
                        builder.AppendLine(func);
                    }
                });
                builder.AppendLine("}");
            }



            return builder;
        }


    }
}


