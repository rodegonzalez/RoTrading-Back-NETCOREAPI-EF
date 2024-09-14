using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IPositionsSession
    {
        Task<List<Session>> GetAllAsync();
        Task<Session?> GetItemByIdAsync(string id);
        Task<Session?> GetLastAsync();
        Task AddItemAsync(Session item);
        Task SaveChangesAsync();
    }
}
