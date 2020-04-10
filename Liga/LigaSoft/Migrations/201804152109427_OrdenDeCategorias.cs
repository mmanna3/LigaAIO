namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdenDeCategorias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categoria", "Orden", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categoria", "Orden");
        }
    }
}
