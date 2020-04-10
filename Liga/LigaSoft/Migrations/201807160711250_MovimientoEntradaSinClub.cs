namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientoEntradaSinClub : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovimientoEntradaSinClub",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movimiento", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientoEntradaSinClub", "Id", "dbo.Movimiento");
            DropIndex("dbo.MovimientoEntradaSinClub", new[] { "Id" });
            DropTable("dbo.MovimientoEntradaSinClub");
        }
    }
}
