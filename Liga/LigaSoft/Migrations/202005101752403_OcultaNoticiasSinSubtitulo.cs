namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OcultaNoticiasSinSubtitulo : DbMigration
    {
        public override void Up()
        {
			Sql("update noticia set Visible = 0 where subtitulo = '' ");
        }
        
        public override void Down()
        {
        }
    }
}
