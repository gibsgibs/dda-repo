using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ddaproj.Pages.Admin
{
    [Authorize(Policy = "SuperAdminAndHigher")]
    [BindProperties]
    public class SuperAdminModel : AdminModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public SuperAdminModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
            : base(userManager, context)
        {

        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ApplicationUser = await _userManager.FindByIdAsync(id);
            if (ApplicationUser == null) return RedirectToPage("Edit");
            return Page();
        }
        public bool IsAdmin()
        {
            var claims = _userManager.GetClaimsAsync(ApplicationUser);
            return claims.Result.Select(c => c.Value).Contains("Admin");
        }
        public async Task<IActionResult> OnPostDeleteUserAsync()
        {
            if (ApplicationUser.Id == _superAdminId)
            {
                ModelState.AddModelError(string.Empty, "Cannot delete superadmin.");
                return Page(); 
            }
            ApplicationUser = await _userManager.FindByIdAsync(ApplicationUser.Id);
            if (ApplicationUser == null)
            {
                ModelState.AddModelError(string.Empty, "Could not find that user.");
                return Page();
            }
            if (!ModelState.IsValid) return Page();
            var deleteUser = await _userManager.DeleteAsync(ApplicationUser);
            if (!deleteUser.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete user.");
                return Page();
            }
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostUpdateAdminStatusAsync()
        {
            if (ApplicationUser.Id == _superAdminId)
            {
                ModelState.AddModelError(string.Empty, "Cannot promote or demote superadmin.");
                return Page();
            }
            ApplicationUser = await _userManager.FindByIdAsync(ApplicationUser.Id);
            if (ApplicationUser == null)
            {
                ModelState.AddModelError(string.Empty, "Could not find that user.");
            }
            if (!ModelState.IsValid) return Page();
            if (!IsAdmin())
            {
                await _userManager.AddClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, "Admin"));
            }
            else if (IsAdmin())
            {
                await _userManager.RemoveClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, "Admin"));
            }
            return RedirectToPage("Edit");
        }
    }
}
