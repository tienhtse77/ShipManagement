using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Ship> Ships { get; }
    DbSet<Port> Ports { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RetryOnExceptionAsync(Func<Task> func);
    void RollbackTransaction();
}
