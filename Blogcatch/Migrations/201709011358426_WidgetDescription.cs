namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WidgetDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Widgets", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Widgets", "Description");
        }
    }
}
