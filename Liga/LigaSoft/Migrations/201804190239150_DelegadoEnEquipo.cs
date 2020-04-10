namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelegadoEnEquipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipo", "Delegado1_Id", c => c.Int());
            AddColumn("dbo.Equipo", "Delegado2_Id", c => c.Int());
            CreateIndex("dbo.Equipo", "Delegado1_Id");
            CreateIndex("dbo.Equipo", "Delegado2_Id");
            AddForeignKey("dbo.Equipo", "Delegado1_Id", "dbo.Delegado", "Id");
            AddForeignKey("dbo.Equipo", "Delegado2_Id", "dbo.Delegado", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipo", "Delegado2_Id", "dbo.Delegado");
            DropForeignKey("dbo.Equipo", "Delegado1_Id", "dbo.Delegado");
            DropIndex("dbo.Equipo", new[] { "Delegado2_Id" });
            DropIndex("dbo.Equipo", new[] { "Delegado1_Id" });
            DropColumn("dbo.Equipo", "Delegado2_Id");
            DropColumn("dbo.Equipo", "Delegado1_Id");
        }
    }
}
