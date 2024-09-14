using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using BK_NetAPI_SQLite.Interfaces;

namespace BK_NetAPI_SQLite.Repositories
{
    public class PositionsSessionRepository : IPositionsSession
    {
        private readonly Db _context;

        public PositionsSessionRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllAsync()
        {
            return await _context.Sessions.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(string id)
        {
            return await _context.Sessions.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }
        public async Task<Session?> GetLastAsync()
        {
            return await _context.Sessions.Where(a => a.Deleted == 0).OrderByDescending(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task AddItemAsync(Session item)
        {
            await _context.Sessions.AddAsync(item);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}
