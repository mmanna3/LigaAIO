using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaSoft.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.Integration
{
	public class BaseIntegrationTest
	{
		protected ApplicationDbContext Context => TestConfig.Context;

		protected BaseIntegrationTest()
		{
		}

		[TearDown]
		public void ResetChangeTracker()
		{
			IEnumerable<DbEntityEntry> changedEntriesCopy = Context.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added ||
				            e.State == EntityState.Modified ||
				            e.State == EntityState.Deleted
				);

			foreach (DbEntityEntry entity in changedEntriesCopy)
			{
				Context.Entry(entity.Entity).State = EntityState.Detached;
			}
		}
	}
}
