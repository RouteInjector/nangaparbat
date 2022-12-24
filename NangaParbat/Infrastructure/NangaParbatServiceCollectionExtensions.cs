using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Entities;
using NangaParbat.Controllers;
using NangaParbat.Models;
using NangaParbat.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NangaParbat.Infrastructure
{
    public static class NangaParbatServiceCollectionExtensions
    {
        public static IServiceCollection AddNangaParbat(this IServiceCollection services, IConfiguration config)
        {
            ConfigureDatabase(config);
            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(ConfigureSwagger());
            services.AddSingleton(typeof(Storage<>));
            services.AddMvc(o => o.Conventions.Add(new GenericControllerRouteConvention()))
                .ConfigureApplicationPartManager(m =>
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));

            return services;
        }
        
        private static void ConfigureDatabase(IConfiguration config)
        {
            var settings = config.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;
            Task.Run(async () => await DB.InitAsync(settings.DatabaseName,
                    MongoClientSettings.FromConnectionString(settings.ConnectionString)))
                .GetAwaiter()
                .GetResult();
        }

        private static Action<SwaggerGenOptions> ConfigureSwagger()
        {
            return c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                        return new[] {api.GroupName};

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        return new[] {controllerActionDescriptor.ControllerName};

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
            };
        }
    }
}