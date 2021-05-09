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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //var result = base.SaveChangesAsync(cancellationToken);
            //return result;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APP_DbContext).Assembly);
        }
    }
}