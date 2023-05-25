namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenombraTitulosDePublicidadesADesktopYBanner : DbMigration
    {
        public override void Up()
        {
            Sql(@"  update publicidad
                    set titulo = 'Desktop'
                    where id = 1

                    update publicidad
                    set titulo = 'Mobile'
                    where id = 2

                    update publicidad
                    set titulo = 'No utilizado'
                    where id = 3

                    update publicidad
                    set titulo = 'No utilizado'
                    where id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
