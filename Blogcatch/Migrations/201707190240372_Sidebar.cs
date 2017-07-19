namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sidebar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sidebars",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sidebars");
        }
    }
}
