using Blogcatch.Areas.Admin.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Blogcatch.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Page> Pages { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}