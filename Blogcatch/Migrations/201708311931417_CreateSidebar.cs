namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSidebar : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[Sidebars] ([Id], [Body]) VALUES (1, N'')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM [dbo].[Sidebars] WHERE [Id] = 1");
        }
    }
}
