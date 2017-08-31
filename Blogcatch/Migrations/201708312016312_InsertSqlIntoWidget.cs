namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertSqlIntoWidget : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Widgets] ON");
            Sql("INSERT INTO [dbo].[Widgets] ([Id], [Sorting], [Enabled], [Type]) VALUES (1, 100, 1, N'Search')");

            Sql("INSERT INTO[dbo].[Widgets]([Id], [Sorting], [Enabled], [Type]) VALUES(2, 100, 1, N'Categories')");
            Sql("SET IDENTITY_INSERT [dbo].[Widgets] OFF");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM [dbo].[Widgets] WHERE [Id] = 1");
            Sql("DELETE FROM [dbo].[Widgets] WHERE [Id] = 2");
        }
    }
}
