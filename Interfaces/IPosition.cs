using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IPosition
    {
        Task<List<PositionView>> GetAllAsync();
        Task<Position?> GetAsync(int id);
        Task<Position> CreateAsync(Position position);
        Task<Position?> UpdateAsync(Position position, int id);
        Task<Position?> DeleteAsync(int id);
        Task<List<PositionView>> GetOpenedPositionsAsync();
        Task<List<PositionView>> GetNotOpenedPositionsAsync();
        Task<Tppblock?> GetTppBlockAsync(int tppid, int tppblocksec);
        Task CreateTppBlockAsync(Tppblock tppblock);
        Task CreateTppBlockSecuenceAsync(Tppblocksecuence tppblocksecuence);
    }
}
