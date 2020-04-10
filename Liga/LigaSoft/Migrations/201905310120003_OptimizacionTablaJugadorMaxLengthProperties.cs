namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptimizacionTablaJugadorMaxLengthProperties : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jugador", new[] { "DNI" });
            AlterColumn("dbo.Jugador", "DNI", c => c.String(nullable: false, maxLength: 9, unicode: false));
            AlterColumn("dbo.Jugador", "Nombre", c => c.String(maxLength: 14, unicode: false));
            AlterColumn("dbo.Jugador", "Apellido", c => c.String(maxLength: 14, unicode: false));
            CreateIndex("dbo.Jugador", "DNI", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jugador", new[] { "DNI" });
            AlterColumn("dbo.Jugador", "Apellido", c => c.String(maxLength: 14));
            AlterColumn("dbo.Jugador", "Nombre", c => c.String(maxLength: 14));
            AlterColumn("dbo.Jugador", "DNI", c => c.String(nullable: false, maxLength: 9));
            CreateIndex("dbo.Jugador", "DNI", unique: true);
        }
    }
}
