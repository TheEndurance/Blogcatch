namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedWidgetDescriptions : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[Widgets] SET [Description] = (N'A search bar used to query for blog posts') WHERE [Id] = 1");

            Sql("UPDATE [dbo].[Widgets] SET [Description] = (N'Displays blog category links to navigate to blog posts in specific categories') WHERE [Id] = 2");
        }
        
        public override void Down()
        {
        }
    }
}
