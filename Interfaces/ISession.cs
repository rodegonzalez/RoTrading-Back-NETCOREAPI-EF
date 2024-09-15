using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface ISession
    {
        Task<List<Session>> GetAllAsync();
        Task<Session?> GetAsync(string id);
        Task<Session?> GetLastAsync();
        Task<Session> CreateAsync(string id);
        Task<Session?> UpdateAsync(Session record, string id);
        Task<Session?> DeleteAsync(string id);
    }
}
