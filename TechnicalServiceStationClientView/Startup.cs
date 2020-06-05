using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using TechnicalServiceStationBusinessLogic.BusinessLogic;
using TechnicalServiceStationBusinessLogic.HelperModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationDatabaseImplement;
using TechnicalServiceStationDatabaseImplement.Implements;

namespace TechnicalServiceStationClientView
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ServiceLogic service = new ServiceLogic();
            AutopartsLogic autoparts = new AutopartsLogic();
            WarehouseLogic warehouse = new WarehouseLogic();

            if ((service.Read(null) == null || service.Read(null).Count == 0) && 
                (autoparts.Read(null) == null || autoparts.Read(null).Count == 0) && 
                (warehouse.Read(null) == null || warehouse.Read(null).Count == 0))
            {
                TechnicalServiceStationDatabase context = new TechnicalServiceStationDatabase();
                context.FillDatabase();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IServiceLogic, ServiceLogic>();
            services.AddTransient<IAutopartsLogic, AutopartsLogic>();
            services.AddTransient<IWarehouseLogic, WarehouseLogic>();
            services.AddTransient<MainLogic>();
            services.AddTransient<ReportLogic>();

            services.AddControllers();
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MailLogic.MailConfig(new MailConfig
            {
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
            });

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Enter}/{id?}");
            });
        }
    }
}
