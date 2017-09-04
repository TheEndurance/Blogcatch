namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageHeaderPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "HeaderPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "HeaderPicture");
        }
    }
}
