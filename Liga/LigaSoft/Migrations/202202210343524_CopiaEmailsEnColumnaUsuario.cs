namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CopiaEmailsEnColumnaUsuario : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE usuariodelegado SET usuario = email;");
        }
        
        public override void Down()
        {
        }
    }
}
