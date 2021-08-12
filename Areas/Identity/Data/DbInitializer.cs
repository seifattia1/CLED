using CLED.Areas.Identity.Data;
using CLED.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Identity
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(CLEDContext context, IServiceProvider serviceProvider,UserManager<CLEDUser> userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] RoleNames = { "Admin", "Moderator", "FreeUser", "PremiumUser" };
            IdentityResult roleResult;
            foreach (var roleName in RoleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }

            }
            string email = "seifeddine.attia.1@esprit.tn";
            string password = "Seifattia1";

            if(userManager.FindByEmailAsync(email).Result == null)
            {
                CLEDUser user = new CLEDUser();
                user.UserName = "SeifEddine";
                user.Email = email;
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

        }
    }
}
