using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NangaParbat.Controllers
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assemblies = new[] {Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly()!};

            foreach (var assembly in assemblies)
            {
                var candidates = assembly.GetExportedTypes().Where(
                    x => x.GetCustomAttributes<GenericCrudAttribute>().Any());

                foreach (var candidate in candidates)
                {
                    feature.Controllers.Add(typeof(GenericCrud<>).MakeGenericType(candidate).GetTypeInfo()
                    );
                }
            }
        }
    }
}