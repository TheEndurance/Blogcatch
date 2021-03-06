namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJumbotronToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "DisplayPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DisplayPicture");
        }
    }
}
