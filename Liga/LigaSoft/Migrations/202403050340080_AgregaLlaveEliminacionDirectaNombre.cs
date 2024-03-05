namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaLlaveEliminacionDirectaNombre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneo", "LlaveEliminacionDirectaNombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Torneo", "LlaveEliminacionDirectaNombre");
        }
    }
}
