using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IDivisa
    {
        Task<List<Divisa>> GetAllAsync();
        Task<Divisa?> GetAsync(int id);
        Task<Divisa?> CreateAsync(Divisa record);
        Task<Divisa?> UpdateAsync(Divisa record, int id);
        Task<Divisa?> DeleteAsync(int id);
    }
}
