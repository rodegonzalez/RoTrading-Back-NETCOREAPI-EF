using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class PositionHighPatternRepository : IPositionHighPattern
    {
        private readonly Db _context;

        public PositionHighPatternRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Position_highpattern>> GetAllAsync()
        {
            return await _context.Position_highpatterns.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Position_highpattern?> GetAsync(int id)
        {
            return await _context.Position_highpatterns.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task<Position_highpattern?> CreateAsync(Position_highpattern record)
        {
            if (record is null) return null;

            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;
            await _context.Position_highpatterns.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Position_highpattern?> UpdateAsync(Position_highpattern updaterecord, int id)
        {
            var record = await this.GetAsync(id);
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

        public async Task<Position_highpattern?> DeleteAsync(int id)
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
