using System.Threading.Tasks;

namespace Infrastructure.Persistence.DbInitializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}