using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Repositories
{
    public class PositionRepository : IPosition
    {
        private readonly Db _context;

        public PositionRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<PositionView>> GetAllPositionsAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0).Take(100).ToListAsync();
        }

        public async Task<List<PositionView>> GetOpenedPositionsAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0 && a.Status == "opened").ToListAsync();
        }

        public async Task<List<PositionView>> GetNotOpenedPositionsAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0 && a.Status != "opened").Take(10).ToListAsync();
        }

        public async Task<Position?> GetPositionByIdAsync(int id)
        {
            return await _context.Positions.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task AddPositionAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
        }

        public async Task<Tppblock?> GetTppBlockAsync(int tppid, int tppblocksec)
        {
            return await _context.TppBlocks
                                 .Where(a => a.Tppid == tppid && a.Tppblocksec == tppblocksec)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddTppBlockAsync(Tppblock tppblock)
        {
            await _context.TppBlocks.AddAsync(tppblock);
        }

        public async Task AddTppBlockSecuenceAsync(Tppblocksecuence tppblocksecuence)
        {
            await _context.TppBlockSecuences.AddAsync(tppblocksecuence);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
