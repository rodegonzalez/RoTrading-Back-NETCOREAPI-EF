using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class TickerRepository : ITicker
    {
        private readonly Db _context;

        public TickerRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Ticker>> GetAllAsync()
        {
            return await _context.Tickers.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Ticker?> GetAsync(int id)
        {
            return await _context.Tickers.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task<Ticker?> CreateAsync(Ticker record)
        {
            if (record is null) return null;

            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;
            await _context.Tickers.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Ticker?> UpdateAsync(Ticker updaterecord, int id)
        {
            //var record = await this.GetAsync(id);
            var record = await _context.Tickers.Where(a => a.Id == id && a.Deleted == 0).FirstAsync();
            if (record is null) return null;

            record.Name = updaterecord.Name;
            record.Description = updaterecord.Description;
            record.Status = updaterecord.Status;
            record.Active = updaterecord.Active;
            record.Note = updaterecord.Note;
            record.Modification = CommonShared.GetMyDateTime();
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Ticker?> DeleteAsync(int id)
        {
            //var record = await this.GetAsync(id);
            var record = await _context.Tickers.Where(a => a.Id == id && a.Deleted == 0).FirstAsync();
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Name = CommonShared.GetDeletedPrefix() + record.Name;
            record.Modification = CommonShared.GetMyDateTime();

            await _context.SaveChangesAsync();
            return record;
        }        

    }
}
