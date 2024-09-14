using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IAccount
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetItemByIdAsync(int id);
        Task AddItemAsync(Account item);
        Task SaveChangesAsync();
    }
}
