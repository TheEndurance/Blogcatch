namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PostCategories", "PostId", "dbo.Posts");
            DropIndex("dbo.PostCategories", new[] { "CategoryId" });
            DropIndex("dbo.PostCategories", new[] { "PostId" });
            AddColumn("dbo.Posts", "Category_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Category_Id");
            AddForeignKey("dbo.Posts", "Category_Id", "dbo.Categories", "Id");
            DropTable("dbo.PostCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.PostId });
            
            DropForeignKey("dbo.Posts", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "Category_Id" });
            DropColumn("dbo.Posts", "Category_Id");
            CreateIndex("dbo.PostCategories", "PostId");
            CreateIndex("dbo.PostCategories", "CategoryId");
            AddForeignKey("dbo.PostCategories", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
