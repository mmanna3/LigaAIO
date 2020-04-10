namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescripcionEnParametrizacionGlobal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametrizacionGlobal", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParametrizacionGlobal", "Descripcion");
        }
    }
}
