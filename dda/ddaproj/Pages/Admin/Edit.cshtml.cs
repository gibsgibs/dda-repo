using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ddaproj.Pages.Admin
{
    public class EditModel : AdminModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IList<string> _customClaims;
        public ApplicationUser ApplicationUser { get; set; }
        public List<string> ObtainedClaims { get; set; }
        public List<string> UnobtainedClaims { get; set; }
        [Display(Name = "Add Role")]
        public string ClaimValueToAdd { get; set; }
        [Display(Name = "Remove Roll")]
        public string ClaimValueToRemove { get; set; }
        
        public EditModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : base(userManager)
        {
            _context = context;
            _customClaims = context.CustomClaims.Select(customClaim => customClaim.Value).ToList();
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ApplicationUser = await _userManager.FindByIdAsync(id);
            if (ApplicationUser == null) return RedirectToPage("Index");
            ObtainedClaims = _userManager.GetClaimsAsync(ApplicationUser).Result.Select(claim => claim.Value).ToList();
            UnobtainedClaims = _customClaims.Where(customClaimValue => !ObtainedClaims.Contains(customClaimValue)).ToList();
            return Page();
        }
        private enum ClaimOptions
        {
            Add,
            Remove
        }
        private async Task UpdateUserClaims(string value, ClaimOptions option)
        {
            if (_customClaims.Contains(value))
            {
                switch (option)
                {
                    case ClaimOptions.Add:
                        await _userManager.AddClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, value)); break;
                    case ClaimOptions.Remove:
                        await _userManager.RemoveClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, value)); break;
                    default: break;
                }
            } 
        }
        public async Task<IActionResult> OnPostUpdateUserAsnyc()
        {
            if (ApplicationUser.Id == _superAdminId || ApplicationUser.Id == _userManager.GetUserId(User)) 
            {
                ModelState.AddModelError("UpdateSelfOrSuperAdmin", "You cannot update yourself.");
                return Page();
            }
            if (!ModelState.IsValid) return Page();
            ApplicationUser = await _userManager.FindByIdAsync(ApplicationUser.Id);
            await UpdateUserClaims(ClaimValueToAdd, ClaimOptions.Add);
            await UpdateUserClaims(ClaimValueToRemove, ClaimOptions.Remove);
            var updateUser = await _userManager.UpdateAsync(ApplicationUser);
            if (!updateUser.Succeeded)
            {
                ModelState.AddModelError("UpdateFailed", "Failed to update user.");
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}