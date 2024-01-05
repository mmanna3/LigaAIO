namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaLeyendaEnCategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categoria", "Leyenda", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categoria", "Leyenda");
        }
    }
}
