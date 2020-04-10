namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaFechaASancion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sancion", "Fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sancion", "Fecha");
        }
    }
}
