using BK_NetAPI_SQLite.Interfaces;
using GeneralStore.Models;
using BK_NetAPI_SQLite.Common;

namespace BK_NetAPI_SQLite.Services
{
    public class PositionService
    {
        private readonly IPosition _repo;

        public PositionService(IPosition positionRepository)
        {
            _repo = positionRepository;
        }

        public async Task<Position> CreatePositionAsync(Position record)
        {
            string _now = CommonShared.GetMyDateTime();
            record.Modification = _now;
            await _repo.AddPositionAsync(record);

            // create tppblock if not exists
            Tppblock? tppblock = await _repo.GetTppBlockAsync(record.Tppid, record.Tppblocksec);

            if (tppblock is null)
            {
                Tppblock newtppblock = new Tppblock
                {
                    Tppid = record.Tppid,
                    Tppblocksec = record.Tppblocksec,
                    Creation = _now,
                    Modification = _now
                };
                await _repo.AddTppBlockAsync(newtppblock);
            }

            // update block secuence
            Tppblocksecuence tppblocksecuence = new Tppblocksecuence
            {
                Positionid = record.Id,
                Sessionid = record.Sessionid,
                Tppid = record.Tppid,
                Tppblocksec = record.Tppblocksec,
                Sec = record.Sec,
                Creation = _now,
                Modification = _now,
            };
            await _repo.AddTppBlockSecuenceAsync(tppblocksecuence);

            // update db
            await _repo.SaveChangesAsync();

            return record;
        }

        public async Task<Position?> UpdatePositionAsync(Position updaterecord, int id)
        {
            string _now = CommonShared.GetMyDateTime();
            var record = await _repo.GetPositionByIdAsync(id);
            if (record is null) return null;

            record.Sessionid = updaterecord.Sessionid;
            record.Guid = updaterecord.Guid;
            record.Tppid = updaterecord.Tppid;
            record.Tppcheck = updaterecord.Tppcheck;
            record.Tppblocksec = updaterecord.Tppblocksec;
            record.Sec = updaterecord.Sec;
            //record.Creation = updaterecord.Creation;
            record.Timein = updaterecord.Timein;
            record.Timeout = updaterecord.Timeout;
            record.Pricein = updaterecord.Pricein;
            record.Priceout = updaterecord.Priceout;
            record.Buysell = updaterecord.Buysell;
            record.Contracts = updaterecord.Contracts;
            record.Opresultticks = updaterecord.Opresultticks;
            record.Usdeur = updaterecord.Usdeur;
            record.Opresult = updaterecord.Opresult;
            record.Commission = updaterecord.Commission;
            record.Opresulteur = updaterecord.Opresulteur;
            record.Divisaid = updaterecord.Divisaid;
            record.Accountid = updaterecord.Accountid;
            record.Tickerid = updaterecord.Tickerid;
            record.Pattern1id = updaterecord.Pattern1id;
            record.Pattern2id = updaterecord.Pattern2id;
            record.Setup1id = updaterecord.Setup1id;
            record.Setup2id = updaterecord.Setup2id;
            //record.Processed = updaterecord.Processed;
            //record.Deleted = updaterecord.Deleted;
            //record.Deletednote = updaterecord.Deletednote;
            record.Imagepath = updaterecord.Imagepath;
            record.Note = updaterecord.Note;
            record.Status = updaterecord.Status;

            record.Modification = _now;

            await _repo.SaveChangesAsync();
            return record;
        }

        public async Task<Position?> DeletePositionAsync(int id)
        {            
            var record = await _repo.GetPositionByIdAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Modification = CommonShared.GetMyDateTime();

            await _repo.SaveChangesAsync();
            return record;
        }

    }
}
