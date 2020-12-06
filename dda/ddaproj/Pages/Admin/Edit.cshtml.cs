using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ddaproj.Pages.Admin
{
    [Authorize(Policy = "AdminAndHigher")]
    [BindProperties]
    public class EditModel : AdminModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Add Role")]
        public string ClaimValueToAdd { get; set; }
        [Display(Name = "Remove Role")]
        public string ClaimValueToRemove { get; set; }
        
        public EditModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) 
            : base(userManager, context)
        {
            _allCustomClaims = _allCustomClaims.Where(acc => acc.Value != "Admin").ToList();
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ApplicationUser = await _userManager.FindByIdAsync(id);
            if (ApplicationUser == null) return RedirectToPage("Index");
            return Page();
        }
        public IList<CustomClaim> GetUnobtainedClaims()
        {
            return _allCustomClaims.Except(GetUsersCustomClaims(ApplicationUser).Result).ToList();
        }
        private enum ClaimOptions
        {
            Add,
            Remove
        }
        private async Task UpdateUserClaims(string name, ClaimOptions option)
        {
            if (_allCustomClaims.Select(cc => cc.Name).Contains(name))
            {
                var claimValue = _allCustomClaims.Where(cc => cc.Name == name).FirstOrDefault().Value;
                switch (option)
                {
                    case ClaimOptions.Add:
                        await _userManager.AddClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, claimValue)); break;
                    case ClaimOptions.Remove:
                        await _userManager.RemoveClaimAsync(ApplicationUser, new Claim(ClaimTypes.Role, claimValue)); break;
                    default: break;
                }
            } 
        }
        public async Task<IActionResult> OnPostUpdateUserAsync()
        {
            ApplicationUser = _userManager.FindByIdAsync(ApplicationUser.Id).Result;
            if (ApplicationUser == null)
            {
                ModelState.AddModelError(string.Empty, "That user does not exist.");
            }
            if (ApplicationUser.Id == _superAdminId || ApplicationUser.Id == _userManager.GetUserId(User)) 
            {
                ModelState.AddModelError(string.Empty, "You cannot update yourself.");
                return Page();
            }
            if (!ModelState.IsValid) return Page();
            await UpdateUserClaims(ClaimValueToAdd, ClaimOptions.Add);
            await UpdateUserClaims(ClaimValueToRemove, ClaimOptions.Remove);
            var updateUser = await _userManager.UpdateAsync(ApplicationUser);
            if (!updateUser.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                return Page();
            }
            return Page();
        }
    }
}
