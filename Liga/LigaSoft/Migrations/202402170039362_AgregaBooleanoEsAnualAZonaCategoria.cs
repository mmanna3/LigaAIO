namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaBooleanoEsAnualAZonaCategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ZonaCategoria", "EsAnual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ZonaCategoria", "EsAnual");
        }
    }
}
