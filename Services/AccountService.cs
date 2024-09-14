using GeneralStore.Common;

using GeneralStore.Models;
using GeneralStore.Repositories;

namespace GeneralStore.Services
{
    public class AccountService
    {
        private readonly AccountRepository _repo;

        public AccountService(AccountRepository repository)
        {
            _repo = repository;
        }

        public async Task<Account?> CreateItemAsync(Account record)
        {
            if (record is null) return null;
            
            var _now = CommonShared.GetMyDateTime();
            record.Active = 1;
            record.Deleted = 0;
            record.Creation = _now;
            record.Modification = _now;                
            await _repo.AddItemAsync(record);
            await _repo.SaveChangesAsync();
            return record;          
        }

        public async Task<Account?> UpdateItemAsync(Account updaterecord, int id)
        {            
            var record = await _repo.GetItemByIdAsync(id);
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
            await _repo.SaveChangesAsync();
            return record;
        }

        public async Task<Account?> DeleteItemAsync(int id)
        {            
            var record = await _repo.GetItemByIdAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Name = CommonShared.GetDeletedPrefix() + record.Name;
            record.Modification = CommonShared.GetMyDateTime();

            await _repo.SaveChangesAsync();
            return record;
        }

    }
}
