using Core.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class ATLSmsContext : DbContext
    {
        public ATLSmsContext(DbContextOptions<ATLSmsContext> options)
        :base(options)
        {

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that implements ICreateTiming,
                // set CreatedAt to current UTC 
                if (entry.Entity is ICreatedAt createdEntity && entry.State == EntityState.Added)
                {
                    createdEntity.CreatedAt = utcNow;
                }

                if (entry.Entity is IUpdatedAt updatedEntity && entry.State == EntityState.Modified)
                {
                    // set the updated date to "now"
                    updatedEntity.UpdatedAt = utcNow;

                    // mark property as "don't touch"
                    // we don't want to update on a Modify operation
                    entry.Property("CreatedAt").IsModified = false;
                }
            }
        }
    }
}
