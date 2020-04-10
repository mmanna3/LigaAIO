namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GolesEsStringParaElNoPresento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Partido", "GolesLocal", c => c.String());
            AlterColumn("dbo.Partido", "GolesVisitante", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partido", "GolesVisitante", c => c.Int(nullable: false));
            AlterColumn("dbo.Partido", "GolesLocal", c => c.Int(nullable: false));
        }
    }
}
