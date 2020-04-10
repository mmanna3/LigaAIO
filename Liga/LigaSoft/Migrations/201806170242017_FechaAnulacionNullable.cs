namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaAnulacionNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movimiento", "FechaAnulacion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movimiento", "FechaAnulacion", c => c.DateTime(nullable: false));
        }
    }
}
