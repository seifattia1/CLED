
using CLED.Areas.Identity.Data;
using CLED.Data;
using CLED.Hubs;
using CLED.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLED
{
    public class Startup
    {
        private IConfigurationRoot _config;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var ConfigBuilder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json");
            _config = ConfigBuilder.Build();
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddSession();
            services.AddSingleton<HttpClient>();
            services.AddDbContext<CLEDContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("CLEDContextConnection")));
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddIdentity<CLEDUser, IdentityRole>(options =>
             {
                 options.Password.RequireNonAlphanumeric = false;
                 options.SignIn.RequireConfirmedAccount = false;
                 options.SignIn.RequireConfirmedEmail = false;


             })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<CLEDContext>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                StripeConfiguration.ApiKey = "sk_test_51JRlz7JGbBjxQ0BEOsH8h7jkYnXPqiJvRTmNtui7lLIfRXiG8hg83aQlk5IS3n0rcBnAbJh3OSCnwoaec8p6AVpB00fFyMN5vi";
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
             //   app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();      
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                  name: "Admin",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
