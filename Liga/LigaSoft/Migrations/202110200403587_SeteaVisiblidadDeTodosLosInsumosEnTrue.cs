namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeteaVisiblidadDeTodosLosInsumosEnTrue : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE ConceptoInsumo SET Visible = 1");
        }
        
        public override void Down()
        {
        }
    }
}
