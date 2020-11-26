using System.Collections.Generic;
using System.Linq;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ddaproj.Pages.Admin
{
    [Authorize(Policy = "AdminAndHigher")]
    public class IndexModel : AdminModel
    {
        public ICollection<ApplicationUser> Users { get; private set; }
        public IndexModel(UserManager<ApplicationUser> userManager) : base(userManager)
        {
            
        }

        public void OnGet()
        {
            Users = _userManager.Users.Where(u => u.Id != _superAdminId).ToList();
        }
    }
}