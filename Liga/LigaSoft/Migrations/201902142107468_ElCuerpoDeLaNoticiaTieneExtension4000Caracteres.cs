namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElCuerpoDeLaNoticiaTieneExtension4000Caracteres : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Noticia", "Cuerpo", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Noticia", "Cuerpo", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
