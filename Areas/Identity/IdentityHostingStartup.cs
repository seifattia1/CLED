﻿using System;
using CLED.Areas.Identity.Data;
using CLED.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CLED.Areas.Identity.IdentityHostingStartup))]
namespace CLED.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CLEDContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CLEDContextConnection")));

                services.AddDefaultIdentity<CLEDUser>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedAccount = false;

                })

                    .AddEntityFrameworkStores<CLEDContext>();
                
            });
        }
    }
}