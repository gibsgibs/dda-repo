using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddaproj.Pages.Admin
{
    public class AdminModel : PageModel
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected IList<CustomClaim> _allCustomClaims;
        protected readonly string _superAdminId;
        public AdminModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _superAdminId = userManager.FindByNameAsync("superadmin").Result.Id;
            _allCustomClaims = context.CustomClaims.ToList();
        }
        public async Task<string> GetCustomClaimsAsStringAsync(ApplicationUser user)
        {
            var usersCustomClaims = await GetUsersCustomClaims(user);
            return string.Join(", ", usersCustomClaims.Select(customClaim => customClaim.Name));
        }
        public async Task<IList<CustomClaim>> GetUsersCustomClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return _allCustomClaims.Where(cc => claims.Any(c => c.Value == cc.Value)).ToList();
        }
    }
}
