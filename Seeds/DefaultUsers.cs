using CLED.Areas.Identity.Data;
using CLED.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CLED.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<CLEDUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new CLEDUser
            {
                UserName = "cledadmin",
                Email = "admin@cled.com",
                FullName = "Admin",
                Avatar = "avatar1.png"

            };
            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Seifattia1");
                await userManager.AddToRolesAsync(defaultUser, new List<string> { Roles.Admin.ToString(), Roles.FreeUser.ToString(), Roles.Moderator.ToString(), Roles.PremiumUser.ToString() }) ;
            }

            await roleManager.SeedClaimsForAdmin();

        }

        public static async Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRoles = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            await roleManager.AddPermissionClaims(adminRoles, "Products");
        }

        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsList(module);

            foreach (var permission in allPermissions)
            {
                if(!allClaims.Any(c=> c.Type == "Permissions" && c.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permissions", permission));
                }
            }
        }
    }
}
