namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agrega_bajalogica_enequipos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipo", "BajaLogica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipo", "BajaLogica");
        }
    }
}
