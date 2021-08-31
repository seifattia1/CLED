using CLED.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.PremiumUser.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.FreeUser.ToString()));

            }

           
        }
    }
}
