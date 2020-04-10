namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParametrosGlobalesEsUnaSolaFilaDeLaBDAhora : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametroGlobal", "ValorPorDefectoEnPesosDelConceptoFichaje", c => c.Int(nullable: false));
            AddColumn("dbo.ParametroGlobal", "EscudoPorDefecto", c => c.String());
            DropColumn("dbo.ParametroGlobal", "Clave");
            DropColumn("dbo.ParametroGlobal", "Valor");
            DropColumn("dbo.ParametroGlobal", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParametroGlobal", "Descripcion", c => c.String());
            AddColumn("dbo.ParametroGlobal", "Valor", c => c.String());
            AddColumn("dbo.ParametroGlobal", "Clave", c => c.String());
            DropColumn("dbo.ParametroGlobal", "EscudoPorDefecto");
            DropColumn("dbo.ParametroGlobal", "ValorPorDefectoEnPesosDelConceptoFichaje");
        }
    }
}
