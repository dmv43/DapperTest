using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DapperProject1.Models;   // пространство имен моделей
using Microsoft.EntityFrameworkCore; // пространство имен EntityFramework
using DapperProject1.ViewModels;

namespace DapperProject1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionMaster = "Server=192.168.50.19,1433;Database=master;User ID=TryUser;Password=335435;";
            services.AddTransient<IMasterConnection, MasterConnection>(provider => new MasterConnection(connectionMaster));
            MasterConnection con = new MasterConnection(connectionMaster);
            con.Execute();
            con.WriteDB();
        
            string connectionString = "Server=192.168.50.19,1433;Database=" + MasterConnection.GetDatabaseName()+ ";User ID=TryUser;Password=335435;";
            services.AddTransient<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(connectionString));
            services.AddTransient<ITeacherViewFabric, TeacherViewFabric>(provider => new TeacherViewFabric());
            services.AddTransient<ITeacherFabric, TeacherFabric>(provider => new TeacherFabric());
            services.Configure<IISOptions>(options => {
                options.AutomaticAuthentication = true;
});
            // Add framework services.
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
          //  SampleData.Initialize(app.ApplicationServices);
        }
    }
}
