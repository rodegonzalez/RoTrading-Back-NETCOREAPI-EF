using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class SessionRepository : GeneralStore.Interfaces.ISession
    {
        private readonly Db _context;

        public SessionRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllAsync()
        {
            return await _context.Sessions.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<Session?> GetAsync(int id)
        {
            return await _context.Sessions.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }
        public async Task<Session?> GetLastAsync()
        {
            return await _context.Sessions.Where(a => a.Deleted == 0).OrderByDescending(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task<Session> CreateAsync(int id)
        {
            Session? session = await this.GetAsync(id);
            if (session is null)
            {
                string _now = CommonShared.GetMyDateTime();
                Session newsession = new Session
                {
                    Id = id,
                    Usdeur = 0,
                    Haspositions = 0,
                    Consolidated = 0,
                    Sessionnote = "",
                    Active = 1,
                    Deleted = 0,
                    Creation = _now,
                    Modification = _now
                };
                await _context.Sessions.AddAsync(newsession);
                await _context.SaveChangesAsync();
                return newsession;
            }
            return session;
        }

        public async Task<Session?> UpdateAsync(Session updaterecord, int id)
        {
            var record = await this.GetAsync(id);
            if (record is null) return null;

            record.Usdeur = updaterecord.Usdeur;
            record.Haspositions = updaterecord.Haspositions;
            record.Consolidated = updaterecord.Consolidated;
            record.Active = updaterecord.Active;
            record.Sessionnote = updaterecord.Sessionnote;
            record.Modification = CommonShared.GetMyDateTime();

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Session?> DeleteAsync(int id)
        {
            var record = await this.GetAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Modification = CommonShared.GetMyDateTime();

            await _context.SaveChangesAsync();
            return record;
        }

    }
}
