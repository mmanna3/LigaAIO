using EfEnumToLookup.LookupGenerator;
using LigaSoft.Models;

namespace LigaSoft.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
		}

	    protected override void Seed(ApplicationDbContext context)
	    {
		    // Microsoft comment says "This method will be called after migrating to the latest version."
		    // However my testing shows that it is called every time the software starts

		    ConvertirEnTablaLosEnumDelCodigoUtilizadosEnElDbContext(context);

		    base.Seed(context);
	    }

	    private static void ConvertirEnTablaLosEnumDelCodigoUtilizadosEnElDbContext(ApplicationDbContext context)
	    {
			var enumToLookup = new EnumToLookup();
			var sql = enumToLookup.GenerateMigrationSql(context);
			sql = AgregarDboEnLaCreacionDeTablasPorSiNoEsElEsquemaDefault(sql);
			context.Database.ExecuteSqlCommand(sql);
			context.SaveChanges();
	    }

	    private static string AgregarDboEnLaCreacionDeTablasPorSiNoEsElEsquemaDefault(string sql)
	    {
		    return sql.Replace("CREATE TABLE [Enum", "CREATE TABLE dbo.[Enum");
	    }
    }
}
