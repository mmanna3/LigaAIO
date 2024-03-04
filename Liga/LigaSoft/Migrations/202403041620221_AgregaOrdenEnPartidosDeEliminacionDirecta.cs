namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaOrdenEnPartidosDeEliminacionDirecta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartidoEliminacionDirecta", "Orden", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartidoEliminacionDirecta", "Orden");
        }
    }
}
