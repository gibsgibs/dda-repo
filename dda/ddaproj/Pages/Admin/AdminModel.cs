using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ddaproj.Pages.Admin
{
    public class AdminModel : PageModel
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly string _superAdminId;
        public AdminModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _superAdminId = userManager.FindByNameAsync("superadmin").Result.Id;
        }
    }
}
