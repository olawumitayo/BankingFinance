using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Data
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=fintraksqlmmbs.database.windows.net;Database=TheCoreBankingAzure;user id=fintrak;password=Password20$;";
            services.AddDbContext<TheCoreBankingContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

    }
}
