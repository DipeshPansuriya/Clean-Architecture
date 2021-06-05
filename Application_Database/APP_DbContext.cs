using Application_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Database
{
    public class APP_DbContext : DbContext
    {
        public APP_DbContext(DbContextOptions<APP_DbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Demo_Customer> Demo_Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            bool contextIsInMemory = base.Database.IsInMemory();
            //var result = base.SaveChangesAsync(cancellationToken);
            //return result;
            if (contextIsInMemory)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var transaction = base.Database.BeginTransaction();
                try
                {
                    var result = await base.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ArgumentException($"Error in APP_DbContext.CS :- " + ex.Message, ex.InnerException);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APP_DbContext).Assembly);
        }
    }
}