using ddaproj.Data;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ddaproj.Pages.Admin
{
    public class EditModel : AdminModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IList<string> _allClaimValues;
        public EditModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : base(userManager)
        {
            _context = context;
            //_allClaimValues = context.Claims.Select(claim => claim.Value).ToList();
        }
        public void OnGet()
        {

        }
    }
}