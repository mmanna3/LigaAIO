namespace LigaSoft.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AnioEnumMapeadoConEFEnumLookUp : DbMigration
    {
        public override void Up()
        {
			Sql("-- sql generated by https://github.com/timabell/ef-enum-to-lookup\r\n\r\nset nocount on;\r\nset xact_abort on; -- rollback on error\r\nbegin tran;\r\nIF OBJECT_ID(\'Enum_Anio\', \'U\') IS NULL\r\nbegin\r\n\tCREATE TABLE [Enum_Anio] (Id int CONSTRAINT PK_Enum_Anio PRIMARY KEY, Name nvarchar(255));\r\n--\texec sys.sp_addextendedproperty @name=N\'MS_Description\', @level0type=N\'SCHEMA\', @level0name=N\'dbo\', @level1type=N\'TABLE\',\r\n--\t\t@level1name=N\'Enum_Anio\', @value=N\'Automatically generated. Contents will be overwritten on app startup. Table & contents generated by https://github.com/timabell/ef-enum-to-lookup\';\r\nend\r\n\r\nCREATE TABLE #lookups (Id int, Name nvarchar(255) COLLATE database_default);\r\nINSERT INTO #lookups (Id, Name) VALUES (2018, N\'A2018\');\r\nINSERT INTO #lookups (Id, Name) VALUES (2019, N\'A2019\');\r\nINSERT INTO #lookups (Id, Name) VALUES (2020, N\'A2020\');\r\n\r\nMERGE INTO [Enum_Anio] dst\r\n\tUSING #lookups src ON src.Id = dst.Id\r\n\tWHEN MATCHED AND src.Name <> dst.Name THEN\r\n\t\tUPDATE SET Name = src.Name\r\n\tWHEN NOT MATCHED THEN\r\n\t\tINSERT (Id, Name)\r\n\t\tVALUES (src.Id, src.Name)\r\n\tWHEN NOT MATCHED BY SOURCE THEN\r\n\t\tDELETE\r\n;\r\nTRUNCATE TABLE #lookups;\r\n\r\nDROP TABLE #lookups;\r\n\r\n IF OBJECT_ID(\'FK_Torneo_Anio\', \'F\') IS NULL ALTER TABLE [Torneo] ADD CONSTRAINT FK_Torneo_Anio FOREIGN KEY ([Anio]) REFERENCES [Enum_Anio] (Id);\r\n\r\ncommit;");
        }
        
        public override void Down()
        {
        }
    }
}
