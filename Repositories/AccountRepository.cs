using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly Db _context;

        public AccountRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Account?> GetItemByIdAsync(int id)
        {
            return await _context.Accounts.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task AddItemAsync(Account item)
        {
            await _context.Accounts.AddAsync(item);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}
