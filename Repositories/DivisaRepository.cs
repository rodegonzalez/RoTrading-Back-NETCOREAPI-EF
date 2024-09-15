using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class DivisaRepository : IDivisa
    {
        private readonly Db _context;

        public DivisaRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Divisa>> GetAllAsync()
        {
            return await _context.Divisas.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Divisa?> GetAsync(int id)
        {
            return await _context.Divisas.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task<Divisa?> CreateAsync(Divisa record)
        {
            if (record is null) return null;

            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;
            await _context.Divisas.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Divisa?> UpdateAsync(Divisa updaterecord, int id)
        {
            var record = await this.GetAsync(id);
            if (record is null) return null;

            record.Name = updaterecord.Name;
            record.Description = updaterecord.Description;
            record.Active = updaterecord.Active;
            record.Note = updaterecord.Note;
            record.Modification = CommonShared.GetMyDateTime();
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Divisa?> DeleteAsync(int id)
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
