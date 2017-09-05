namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        PostedDate = c.DateTime(nullable: false),
                        Body = c.String(nullable: false, maxLength: 500),
                        PostId = c.Int(nullable: false),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.AuthorId)
                .Index(t => t.PostId)
                .Index(t => t.ParentCommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.Comments");
        }
    }
}
