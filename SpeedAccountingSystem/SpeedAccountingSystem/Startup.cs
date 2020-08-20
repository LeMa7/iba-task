using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeedAccountingSystem.Repositories;
using SpeedAccountingSystem.RepositoryInterfaces;
using SpeedAccountingSystem.ServiceInterfaces;
using SpeedAccountingSystem.Services;

namespace SpeedAccountingSystem
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
            services.AddControllers();
            services.AddTransient<ISpeedSystemService, SpeedSystemService>();
            services.AddTransient<ISpeedSystemRepository, SpeedSystemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Production";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SpeedSystem}/{action=GetOverSpeedForDay}/{day}/{speed}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SpeedSystem}/{action=GetMinAndMaxForDay}/{day}");
            });
        }
    }
}