namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaPosicionAPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publicidad", "Posicion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publicidad", "Posicion");
        }
    }
}
