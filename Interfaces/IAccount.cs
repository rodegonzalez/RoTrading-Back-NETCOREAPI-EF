using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IAccount
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetAsync(int id);
        Task<Account?> CreateAsync(Account record);
        Task<Account?> UpdateAsync(Account record, int id);
        Task<Account?> DeleteAsync(int id);
    }
}
