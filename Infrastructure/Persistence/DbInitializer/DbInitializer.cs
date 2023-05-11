using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

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
            if (_context.Database.IsRelational() && (await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migrated database");
            }

            if (!_context.Ships.Any())
            {
                await _context.Ships.AddRangeAsync(
                    new Ship("Titanic") {  Velocity = 30, Location = new Point(2.345672, 4.342342) },
                    new Ship("Mayflower") { Velocity = 25, Location = new Point(9.345672, 36.342342) },
                    new Ship("Santa Maria") { Velocity = 12, Location = new Point(20.345672, 47.342342) }
                );

                await _context.SaveChangesAsync();
            }

            if (!_context.Ports.Any())
            {
                await _context.Ports.AddRangeAsync(
                    new Port("Alexandria Pharos", new Point(1.345672, 3.342342)),
                    new Port("Caesarea Maritima", new Point(6.345672, 7.342342)),
                    new Port("Carthage", new Point(3.345672, 4.342342))
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}