namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EquipoEnGoleador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goleador", "EquipoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Goleador", "EquipoId");
            AddForeignKey("dbo.Goleador", "EquipoId", "dbo.Equipo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goleador", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.Goleador", new[] { "EquipoId" });
            DropColumn("dbo.Goleador", "EquipoId");
        }
    }
}
