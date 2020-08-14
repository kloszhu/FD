using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FD.Vue.WebapiService
{
    public interface IWebapiControllerActionService
    {
        IEnumerable<dynamic> List();
    }

    public class WebapiControllerActionService
    {
        private IActionDescriptorCollectionProvider _actionProvider;

        public WebapiControllerActionService(IActionDescriptorCollectionProvider _action) {
            _actionProvider = _action;
        }

        public IEnumerable<dynamic> List()
        {
            var actionDescs = _actionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>().Select(x => new
            {
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                DisplayName = x.DisplayName,
                RouteTemplate = x.AttributeRouteInfo.Template,
                Attributes = x.MethodInfo.CustomAttributes.Select(z => new {
                    TypeName = z.AttributeType.FullName,
                    ConstructorArgs = z.ConstructorArguments.Select(v => new {
                        ArgumentValue = v.Value
                    }),
                    NamedArguments = z.NamedArguments.Select(v => new {
                        v.MemberName,
                        TypedValue = v.TypedValue.Value,
                    }),
                }),
                ActionId = x.Id,
                x.RouteValues,
                Parameters = x.Parameters.Select(z => new {
                    z.Name,
                    TypeName = z.ParameterType.Name,
                })
            });

            return actionDescs;
        }
    }
}
