namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValorDeLaCuota : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipo", "ValorDeLaCuota", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipo", "ValorDeLaCuota");
        }
    }
}
