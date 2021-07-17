using Application_Domain.UserConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Database
{
    public class APP_DbContext : DbContext
    {
        public APP_DbContext()
        {
        }

        public APP_DbContext(DbContextOptions<APP_DbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<user_cls> Users { get; set; }
        public virtual DbSet<role_cls> Roles { get; set; }
        public virtual DbSet<rights_cls> Rights { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (base.Database.IsSqlServer())
            {
                Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = base.Database.BeginTransaction();
                try
                {
                    int result = await base.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ArgumentException($"Error in APP_DbContext.CS :- " + ex.Message, ex.InnerException);
                }
            }
            else
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APP_DbContext).Assembly);
        }
    }
}