using System.Data.Entity;
using LigaSoft.Models;
using NUnit.Framework;

namespace Tests.Integration
{
	[SetUpFixture]
	public class TestConfig
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
