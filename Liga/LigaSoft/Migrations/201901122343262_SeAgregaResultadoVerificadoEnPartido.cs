namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaResultadoVerificadoEnPartido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partido", "ResultadoVerificado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partido", "ResultadoVerificado");
        }
    }
}
