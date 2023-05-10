using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _logger = this.GetService<ILogger<ApplicationDbContext>>();
        }

        public IDbContextTransaction? GetCurrentTransaction { get; private set; }

        public DbSet<Ship> Ships => Set<Ship>();

        public DbSet<Port> Ports => Set<Port>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception sqlException)
            {
                _logger.LogError(sqlException.HResult, sqlException, "Sql exception: {SqlException}", sqlException.Message);
                throw;
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public async Task BeginTransactionAsync()
        {
            GetCurrentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy()
                          .ExecuteAsync(func);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                GetCurrentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (GetCurrentTransaction != null)
                {
                    GetCurrentTransaction.Dispose();
                    GetCurrentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                GetCurrentTransaction?.Rollback();
            }
            finally
            {
                if (GetCurrentTransaction != null)
                {
                    GetCurrentTransaction.Dispose();
                    GetCurrentTransaction = null;
                }
            }
        }
    }
}
