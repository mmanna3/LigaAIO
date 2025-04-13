namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaAniosDesdeHastaEnCategorias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categoria", "AnioNacimientoDesde", c => c.Int());
            AddColumn("dbo.Categoria", "AnioNacimientoHasta", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categoria", "AnioNacimientoHasta");
            DropColumn("dbo.Categoria", "AnioNacimientoDesde");
        }
    }
}
