namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrlEnPublicidadNoEsRequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publicidad", "Url", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publicidad", "Url", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
