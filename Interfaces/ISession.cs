using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface ISession
    {
        Task<List<Session>> GetAllAsync();
        Task<Session?> GetAsync(int id);
        Task<Session?> GetLastAsync();
        Task<Session> CreateAsync(int id);
        Task<Session?> UpdateAsync(Session record, int id);
        Task<Session?> DeleteAsync(int id);
    }
}
