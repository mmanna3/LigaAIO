namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaSubtituloANoticia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Noticia", "Subtitulo", c => c.String(nullable: false, maxLength: 140));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Noticia", "Subtitulo");
        }
    }
}
