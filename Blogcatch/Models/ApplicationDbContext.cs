using System.Data.Entity;
using Blogcatch.Areas.Admin.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blogcatch.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Sidebar> Sidebar { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure domain classes
            modelBuilder.Entity<Comment>().HasRequired(x=>x.Post).WithMany(x=>x.Comments).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}