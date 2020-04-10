namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DNIMaximoNueveDigitos : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jugador", new[] { "DNI" });
            AlterColumn("dbo.Jugador", "DNI", c => c.String(nullable: false, maxLength: 9));
            CreateIndex("dbo.Jugador", "DNI", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jugador", new[] { "DNI" });
            AlterColumn("dbo.Jugador", "DNI", c => c.String(nullable: false, maxLength: 14));
            CreateIndex("dbo.Jugador", "DNI", unique: true);
        }
    }
}
