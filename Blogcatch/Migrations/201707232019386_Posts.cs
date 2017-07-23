namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Posts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        PostDate = c.DateTime(nullable: false),
                        PostModifiedDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        Title = c.String(maxLength: 100),
                        Slug = c.String(maxLength: 100),
                        Excerpt = c.String(),
                        AllowComments = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropTable("dbo.Posts");
        }
    }
}
