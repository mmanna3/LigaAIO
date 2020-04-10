namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElTorneoDeUnEquipoEsNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Equipo", new[] { "TorneoId" });
            AlterColumn("dbo.Equipo", "TorneoId", c => c.Int());
            CreateIndex("dbo.Equipo", "TorneoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Equipo", new[] { "TorneoId" });
            AlterColumn("dbo.Equipo", "TorneoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipo", "TorneoId");
        }
    }
}
