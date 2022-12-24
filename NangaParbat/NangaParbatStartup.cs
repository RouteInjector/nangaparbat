using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using NangaParbat.Infrastructure;
using NangaParbat.Models;

namespace NangaParbat
{
    public class NangaParbatStartup
    {
        public NangaParbatStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;
            Task.Run(async () => await DB.InitAsync(settings.DatabaseName,
                    MongoClientSettings.FromConnectionString(settings.ConnectionString)))
                .GetAwaiter()
                .GetResult();

            services.AddNangaParbat(Configuration);
        }

    }
}