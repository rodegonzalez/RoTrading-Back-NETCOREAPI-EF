using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IPosition
    {
        Task<List<PositionView>> GetAllPositionsAsync();
        Task<List<PositionView>> GetOpenedPositionsAsync();
        Task<List<PositionView>> GetNotOpenedPositionsAsync();
        Task<Position?> GetPositionByIdAsync(int id);
        Task AddPositionAsync(Position position);
        Task<Tppblock?> GetTppBlockAsync(int tppid, int tppblocksec);
        Task AddTppBlockAsync(Tppblock tppblock);
        Task AddTppBlockSecuenceAsync(Tppblocksecuence tppblocksecuence);
        Task SaveChangesAsync();
    }
}
