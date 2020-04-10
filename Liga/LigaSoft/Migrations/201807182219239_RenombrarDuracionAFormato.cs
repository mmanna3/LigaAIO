namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenombrarDuracionAFormato : DbMigration
    {
        public override void Up()
        {
			DropForeignKey("dbo.TorneoTipo", "FK_TorneoTipo_Duracion");
			AddColumn("dbo.TorneoTipo", "Formato", c => c.Int(nullable: false));
            DropColumn("dbo.TorneoTipo", "Duracion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TorneoTipo", "Duracion", c => c.Int(nullable: false));
            DropColumn("dbo.TorneoTipo", "Formato");
        }
    }
}
