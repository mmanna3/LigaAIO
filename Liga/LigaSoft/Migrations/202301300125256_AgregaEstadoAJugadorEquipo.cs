namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEstadoAJugadorEquipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorEquipo", "Estado", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorEquipo", "Estado");
        }
    }
}
