using GeneralStore.Models;

namespace BK_NetAPI_SQLite.Interfaces
{
    public interface IPositionsSession
    {
        Task<List<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(string id);
        Task<Session?> GetLastAsync();
        Task AddItemAsync(Session item);
        Task SaveChangesAsync();
    }
}
