namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TorneoActivoEnVezDeActiva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneo", "Activo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Torneo", "Activa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Torneo", "Activa", c => c.Boolean(nullable: false));
            DropColumn("dbo.Torneo", "Activo");
        }
    }
}
