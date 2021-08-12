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
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CLEDContext>();
                    var userManager = services.GetRequiredService<UserManager<CLEDUser>>();
                    DbInitializer.InitializeAsync(context, services, userManager).Wait();   
                }catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An Error Occured while seeding the Database");
                }
            }
            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args)=>
        
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        
    }
}
