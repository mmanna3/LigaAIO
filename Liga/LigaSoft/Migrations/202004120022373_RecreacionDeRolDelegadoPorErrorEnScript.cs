namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreacionDeRolDelegadoPorErrorEnScript : DbMigration
    {
        public override void Up()
        {
			Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES ('2102983b-31df-46d7-8d11-30b244e9a65a', 'Delegado')");
        }
        
        public override void Down()
        {
        }
    }
}
