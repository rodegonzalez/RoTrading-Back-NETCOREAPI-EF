using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface ITpp
    {
        Task<List<Tpp>> GetAllAsync();
        Task<Tpp?> GetAsync(int id);
        Object GetSecuenceAsync(int id);
        Task<Tpp?> CreateAsync(Tpp record);
        Task<Tpp?> UpdateAsync(Tpp record, int id);
        Task<Tpp?> DeleteAsync(int id);
    }
}
