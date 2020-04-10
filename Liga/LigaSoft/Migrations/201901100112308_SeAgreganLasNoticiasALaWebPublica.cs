namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgreganLasNoticiasALaWebPublica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Noticia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 60),
                        Cuerpo = c.String(nullable: false, maxLength: 400),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Noticia");
        }
    }
}
