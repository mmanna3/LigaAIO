namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelegadoEnEquipoModificacion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Equipo", name: "Delegado1_Id", newName: "Delegado1Id");
            RenameColumn(table: "dbo.Equipo", name: "Delegado2_Id", newName: "Delegado2Id");
            RenameIndex(table: "dbo.Equipo", name: "IX_Delegado1_Id", newName: "IX_Delegado1Id");
            RenameIndex(table: "dbo.Equipo", name: "IX_Delegado2_Id", newName: "IX_Delegado2Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Equipo", name: "IX_Delegado2Id", newName: "IX_Delegado2_Id");
            RenameIndex(table: "dbo.Equipo", name: "IX_Delegado1Id", newName: "IX_Delegado1_Id");
            RenameColumn(table: "dbo.Equipo", name: "Delegado2Id", newName: "Delegado2_Id");
            RenameColumn(table: "dbo.Equipo", name: "Delegado1Id", newName: "Delegado1_Id");
        }
    }
}
