using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface ITicker
    {
        Task<List<Ticker>> GetAllAsync();
        Task<Ticker?> GetAsync(int id);
        Task<Ticker?> CreateAsync(Ticker record);
        Task<Ticker?> UpdateAsync(Ticker record, int id);
        Task<Ticker?> DeleteAsync(int id);
    }
}
