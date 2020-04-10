namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EquiposLibresEInterzonalesEnJornadas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jornada", "QuedoLibre", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jornada", "JuegaInterzonal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jornada", "JuegaInterzonal");
            DropColumn("dbo.Jornada", "QuedoLibre");
        }
    }
}
