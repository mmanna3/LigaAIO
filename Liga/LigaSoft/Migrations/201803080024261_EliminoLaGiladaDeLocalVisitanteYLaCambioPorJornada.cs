namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminoLaGiladaDeLocalVisitanteYLaCambioPorJornada : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LocalVisitante", newName: "Jornada");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Jornada", newName: "LocalVisitante");
        }
    }
}
