namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeCambiaNombreDePropertyTorneoDeActivoAPublicoOculto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneo", "Publico", c => c.Boolean(nullable: false));
            DropColumn("dbo.Torneo", "Activo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Torneo", "Activo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Torneo", "Publico");
        }
    }
}
