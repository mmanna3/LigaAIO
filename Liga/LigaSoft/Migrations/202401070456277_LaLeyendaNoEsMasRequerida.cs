namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaLeyendaNoEsMasRequerida : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZonaCategoria", "Leyenda", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZonaCategoria", "Leyenda", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
