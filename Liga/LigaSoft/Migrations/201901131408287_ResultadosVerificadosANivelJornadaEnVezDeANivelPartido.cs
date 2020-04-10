namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultadosVerificadosANivelJornadaEnVezDeANivelPartido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jornada", "ResultadosVerificados", c => c.Boolean(nullable: false));
            DropColumn("dbo.Partido", "ResultadoVerificado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partido", "ResultadoVerificado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Jornada", "ResultadosVerificados");
        }
    }
}
