namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientoVigente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimiento", "Vigente", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimiento", "Vigente");
        }
    }
}
