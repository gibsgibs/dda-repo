using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ddaproj.Utilities
{
    public class IdentityDataInitializer
    {
        public static void SeedSuperAdmin(UserManager<ApplicationUser> userManager)
        {
            var SuperAdminUsername = Startup.StaticConfiguration["SuperAdmin:Username"];
            if (userManager.FindByNameAsync(SuperAdminUsername).Result != null)
            {
                return;
            }
            var user = new ApplicationUser
            {
                UserName = SuperAdminUsername,
                Email = Startup.StaticConfiguration["SuperAdmin:Email"],
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, Startup.StaticConfiguration["SuperAdmin:Password"]).Result;
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "SuperAdmin")).Wait();
            }
        }
    }
}
