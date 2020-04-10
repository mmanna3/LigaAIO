namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminaEnumAnioGeneradaEnMigracionAnteriorPorqueDeEsoSeEncargaEFToLookup : DbMigration
    {
        public override void Up()
        {
	        Sql(@"	alter table torneo
					drop constraint FK_Torneo_Anio
					
					IF OBJECT_ID('valentia_admin.Enum_Anio', 'U') IS NOT NULL
						drop table valentia_admin.Enum_Anio
					IF OBJECT_ID('Enum_Anio', 'U') IS NOT NULL
						drop table enum_anio
				");
        }
        
        public override void Down()
        {
        }
    }
}
