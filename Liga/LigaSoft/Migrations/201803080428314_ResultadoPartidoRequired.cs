namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultadoPartidoRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Partido", "GolesLocal", c => c.String(nullable: false));
            AlterColumn("dbo.Partido", "GolesVisitante", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partido", "GolesVisitante", c => c.String());
            AlterColumn("dbo.Partido", "GolesLocal", c => c.String());
        }
    }
}
