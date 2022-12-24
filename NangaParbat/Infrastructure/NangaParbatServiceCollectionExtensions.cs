using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(ConfigureSwagger());
            services.AddSingleton(MongoDatabase(config));
            services.AddSingleton(typeof(IDocumentStore<>), typeof(DocumentCollection<>));
            services.AddMvc(o => o.Conventions.Add(new GenericControllerRouteConvention()))
                .ConfigureApplicationPartManager(m =>
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));

            return services;
        }

        private static IMongoDatabase MongoDatabase(IConfiguration config)
        {
            var settings = config.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;
            return new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
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
                
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            };
        }
    }
}