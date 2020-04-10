namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CantFechasAdeudadasEnSanciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sancion", "CantidadFechasQueAdeuda", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sancion", "CantidadFechasQueAdeuda");
        }
    }
}
