using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
    }
}