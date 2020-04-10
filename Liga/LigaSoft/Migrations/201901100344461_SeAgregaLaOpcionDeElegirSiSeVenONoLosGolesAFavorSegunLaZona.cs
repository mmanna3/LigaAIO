namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaLaOpcionDeElegirSiSeVenONoLosGolesAFavorSegunLaZona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zona", "VerGolesEnTabla", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zona", "VerGolesEnTabla");
        }
    }
}
