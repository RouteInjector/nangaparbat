using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NangaParbat.Controllers
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) 
                return;
            
            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var customNameAttribute = genericType.GetCustomAttribute<GenericCrudAttribute>();

            if (customNameAttribute?.Route != null)
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(
                        new RouteAttribute(customNameAttribute.Route))
                });

                controller.ApiExplorer = new ApiExplorerModel
                {
                    
                    GroupName = customNameAttribute.Group,
                    IsVisible = true
                };
            }
        }
    }
}