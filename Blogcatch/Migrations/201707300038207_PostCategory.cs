namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.PostId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostCategories", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostCategories", new[] { "PostId" });
            DropIndex("dbo.PostCategories", new[] { "CategoryId" });
            DropTable("dbo.PostCategories");
        }
    }
}
