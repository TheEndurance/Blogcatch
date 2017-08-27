namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyCategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Posts", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Posts", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "CategoryId");
            AddForeignKey("dbo.Posts", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            AlterColumn("dbo.Posts", "CategoryId", c => c.Int());
            AlterColumn("dbo.Posts", "Title", c => c.String(maxLength: 100));
            RenameColumn(table: "dbo.Posts", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Posts", "Category_Id");
            AddForeignKey("dbo.Posts", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
