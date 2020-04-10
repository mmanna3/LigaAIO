namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CopiarDelegadosDelClubAlEquipo : DbMigration
    {
        public override void Up()
        {
			Sql(@"
					UPDATE equipo 
					SET equipo.Delegado1Id = delegado.Id
					FROM equipo
					JOIN delegado
					ON equipo.clubId = delegado.ClubId
				");
        }
        
        public override void Down()
        {
        }
    }
}
