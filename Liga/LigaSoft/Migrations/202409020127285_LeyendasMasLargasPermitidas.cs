namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeyendasMasLargasPermitidas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZonaCategoria", "Leyenda", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZonaCategoria", "Leyenda", c => c.String(maxLength: 255));
        }
    }
}
