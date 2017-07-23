namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Posts1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PostModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "PostModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
