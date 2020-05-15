using System.Data.Entity;
using LigaSoft.Models;
using NUnit.Framework;
using Tests.Unit.Utilidades;

namespace Tests.Unit
{
	[SetUpFixture]
	public class OneTimeSetUp
	{
		internal static ApplicationDbContext Context = new ApplicationDbContext();

		[OneTimeSetUp]
		public void Initialize()
		{
			Database.SetInitializer(new DropCreateDatabaseAlwaysAndSeed());
			Context.Database.Initialize(true);
		}
	}
}
