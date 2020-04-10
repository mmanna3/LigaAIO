namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSancionFechaADia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sancion", "Dia", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sancion", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sancion", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sancion", "Dia");
        }
    }
}
