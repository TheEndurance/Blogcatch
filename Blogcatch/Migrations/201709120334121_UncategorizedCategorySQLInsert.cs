namespace Blogcatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UncategorizedCategorySQLInsert : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Categories] ON");
            Sql(
                "INSERT INTO[dbo].[Categories]([Id], [Name], [Slug], [Sorting]) VALUES(1, N'Uncategorized', N'uncategorized', 100)");
            Sql("SET IDENTITY_INSERT[dbo].[Categories] OFF");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM [dbo].[Categories] WHERE [Id] = 1");
        }
    }
}
