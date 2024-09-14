using GeneralStore.Repositories;
using GeneralStore.Models;
using GeneralStore.Common;

namespace BK_NetAPI_SQLite.Services
{
    public class PositionsSessionService
    {
        private readonly PositionsSessionRepository _repo;

        public PositionsSessionService(PositionsSessionRepository repository)
        {
            _repo = repository;
        }

        public async Task<Session> CreateItemAsync(string id)
        {            
            Session? session = await _repo.GetItemByIdAsync(id);
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
                await _repo.AddItemAsync(newsession);
                // update db
                await _repo.SaveChangesAsync();
                return newsession;
            }
            return session;           
        }

        public async Task<Session?> UpdateItemAsync(Session updaterecord, string id)
        {            
            var record = await _repo.GetItemByIdAsync(id);
            if (record is null) return null;

            record.Usdeur = updaterecord.Usdeur;
            record.Haspositions = updaterecord.Haspositions;
            record.Consolidated = updaterecord.Consolidated;
            record.Active = updaterecord.Active;
            record.Sessionnote = updaterecord.Sessionnote;
            record.Modification = CommonShared.GetMyDateTime();

            await _repo.SaveChangesAsync();
            return record;
        }

        public async Task<Session?> DeleteItemAsync(string id)
        {            
            var record = await _repo.GetItemByIdAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Modification = CommonShared.GetMyDateTime();

            await _repo.SaveChangesAsync();
            return record;
        }

    }
}
