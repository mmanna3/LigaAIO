namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasarDelegadosDeTablaClubATablaDelegado : DbMigration
    {
        public override void Up()
        {
			Sql("insert into delegado (Nombre, Telefono, ClubId)\r\nselect delegado, telefono, id from club");
        }
        
        public override void Down()
        {
        }
    }
}
