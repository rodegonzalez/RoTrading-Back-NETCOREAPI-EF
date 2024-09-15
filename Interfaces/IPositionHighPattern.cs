using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IPositionHighPattern
    {
        Task<List<Position_highpattern>> GetAllAsync();
        Task<Position_highpattern?> GetAsync(int id);
        Task<Position_highpattern?> CreateAsync(Position_highpattern record);
        Task<Position_highpattern?> UpdateAsync(Position_highpattern record, int id);
        Task<Position_highpattern?> DeleteAsync(int id);
    }
}
