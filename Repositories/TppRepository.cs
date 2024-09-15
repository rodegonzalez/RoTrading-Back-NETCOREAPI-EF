using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class TppRepository : ITpp
    {
        private readonly Db _context;

        public TppRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Tpp>> GetAllAsync()
        {
            return await _context.Tpps.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Tpp?> GetAsync(int id)
        {
            return await _context.Tpps.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public Object GetSecuenceAsync(int id)
        {
            // Obtener el valor máximo de tppblock para el tppid dado
            var maxTppBlock = _context.TppBlocks
                                        .Where(tb => tb.Tppid == id)
                                        .Max(tb => (int?)tb.Tppblocksec);

            if (maxTppBlock == null)
            {
                return Results.NotFound("No se encontró ningún tppblock para el tppid proporcionado.");
            }

            // Obtener el valor máximo de tppblocksecuence para el tppblock máximo
            var maxTppBlockSecuence =  _context.TppBlockSecuences
                                              .Where(tbs => tbs.Tppid == id && tbs.Tppblocksec == maxTppBlock)
                                              .Max(tbs => (int?)tbs.Sec);

            var newsec = new 
            {
                Tppid = id,
                Tppblocksec = maxTppBlock,
                Sec = maxTppBlockSecuence,
            };
            return Results.Ok(newsec);
        }

        public async Task<Tpp?> CreateAsync(Tpp record)
        {
            if (record is null) return null;

            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;
            await _context.Tpps.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Tpp?> UpdateAsync(Tpp updaterecord, int id)
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

        public async Task<Tpp?> DeleteAsync(int id)
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
