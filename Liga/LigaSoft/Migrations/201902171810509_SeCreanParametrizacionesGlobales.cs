namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeCreanParametrizacionesGlobales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametrizacionGlobal",
                c => new
                    {
                        Clave = c.String(nullable: false, maxLength: 128),
                        Valor = c.String(),
                    })
                .PrimaryKey(t => t.Clave);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParametrizacionGlobal");
        }
    }
}
