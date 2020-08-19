using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;

namespace FD.Vue.Conventions
{
    public class MyRouteModel
    {
  

        public string urlString { get; set; }
        public IEnumerable<string> method { get; set; }
        public string actionName { get; set; }
        public string controllerName { get; set; }
        public bool isParampter { get; set; }
        public List<ParameterModel> Parampters { get; set; }
    }

  


}