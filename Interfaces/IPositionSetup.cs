using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IPositionSetup
    {
        Task<List<Position_setup>> GetAllAsync();
        Task<Position_setup?> GetAsync(int id);
        Task<Position_setup?> CreateAsync(Position_setup record);
        Task<Position_setup?> UpdateAsync(Position_setup record, int id);
        Task<Position_setup?> DeleteAsync(int id);
    }
}
