using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

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

        public async Task<Account?> GetAsync(int id)
        {
            return await _context.Accounts.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task<Account?> CreateAsync(Account record)
        {
            if (record is null) return null;

            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;
            await _context.Accounts.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Account?> UpdateAsync(Account updaterecord, int id)
        {
            var record = await this.GetAsync(id);
            if (record is null) return null;

            record.Name = updaterecord.Name;
            record.Description = updaterecord.Description;
            record.Status = updaterecord.Status;
            record.Active = updaterecord.Active;
            record.Note = updaterecord.Note;
            record.Amount_initial = updaterecord.Amount_initial;
            record.Amount_current = updaterecord.Amount_current;
            record.Divisaid = updaterecord.Divisaid;
            record.Acctype = updaterecord.Acctype;
            record.Modification = CommonShared.GetMyDateTime();
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Account?> DeleteAsync(int id)
        {
            var record = await this.GetAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Name = CommonShared.GetDeletedPrefix() + record.Name;
            record.Modification = CommonShared.GetMyDateTime();

            await _context.SaveChangesAsync();
            return record;
        }        

    }
}
