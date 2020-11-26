using System.Collections.Generic;
using ddaproj.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ddaproj.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CustomClaim> CustomClaims { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var customClaims = new List<CustomClaim>()
            {
                new CustomClaim { Id = 1, Name = "Board Member", Value = "BoardMember" },
                new CustomClaim { Id = 2, Name = "Business Member", Value = "BusinessMember" },
                new CustomClaim { Id = 3, Name = "General Member", Value = "GeneralMember" }
            };
            customClaims.ForEach(claim => builder.Entity<CustomClaim>().HasData(claim));
        }
    }
}
