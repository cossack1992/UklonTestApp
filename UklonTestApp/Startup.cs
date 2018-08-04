
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using UklonTestApp.Exensions;
using UklonTestApp.Structure.DataService;
using UklonTestApp.Structure.DataService.DataService;
using UklonTestApp.Structure.Service;
using UklonTestApp.Structure.TrafficStructure.Services;

namespace UklonTestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (var client = new DatabaseContext())
            {
                client.Database.EnsureCreated();
            }
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddMvc();
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
            services.AddSingleton(new LoggerFactory().AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt")));

            services.AddTrafficService(Configuration);
            services.AddSingleton<DatabaseContext>();
            services.AddSingleton<ITrafficStatusProvider, TrafficStatusProvider>();
            services.AddSingleton<ITrafficDataService, TrafficDataService>();
            services.AddSingleton<IService, ServiceAgent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{regionCode?}");
            });
        }
    }
}
