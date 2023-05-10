using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(ILogger<DbInitializer> logger,
                             ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migrated database");
            }
        }
    }
}