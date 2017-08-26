namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePostColumnJumbotronName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "DisplayPicture", c => c.String());
            DropColumn("dbo.Posts", "Jumbotron");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Jumbotron", c => c.String());
            DropColumn("dbo.Posts", "DisplayPicture");
        }
    }
}
