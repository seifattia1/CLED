using CLED.Areas.Identity;
using CLED.Areas.Identity.Data;
using CLED.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerProvider>();
            var logger = loggerFactory.CreateLogger("app");
            try
            {
                var context = services.GetRequiredService<CLEDContext>();
                var userManager = services.GetRequiredService<UserManager<CLEDUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await Seeds.DefaultRoles.SeedAsync(roleManager);
                await Seeds.DefaultUsers.SeedAdminUserAsync(userManager, roleManager);
                await Seeds.DefaultRoles.SeedAsync(roleManager);

                logger.LogInformation("Data Seeded");
                logger.LogInformation("Application Started");
            }
            catch (System.Exception ex)
            {

                logger.LogWarning(ex, "An Error occured while seeding data");
            }



            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>

             WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .Build();

    }
    }

