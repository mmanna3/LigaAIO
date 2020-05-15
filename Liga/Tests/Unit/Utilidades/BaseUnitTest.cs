using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using LigaSoft.Models;
using NUnit.Framework;

namespace Tests.Unit.Utilidades
{
	public class BaseUnitTest
	{
		protected ApplicationDbContext Context => OneTimeSetUp.Context;

		protected BaseUnitTest()
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
