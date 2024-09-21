using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;

namespace GeneralStore.Repositories
{
    public class PositionRepository : IPosition
    {
        private readonly Db _context;

        public PositionRepository(Db context)
        {
            _context = context;
        }

        public async Task<List<PositionView>> GetAllAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0).ToListAsync();
        }

        public async Task<List<PositionView>> GetOpenedPositionsAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0 && a.Status == "opened").ToListAsync();
        }

        public async Task<List<PositionView>> GetNotOpenedPositionsAsync()
        {
            return await _context.PositionViews.Where(a => a.Deleted == 0 && a.Status != "opened").Take(10).ToListAsync();
        }

        public async Task<Position?> GetAsync(int id)
        {
            return await _context.Positions.Where(a => a.Id == id && a.Deleted == 0).FirstOrDefaultAsync();
        }

        public async Task<Position> CreateAsync(Position record)
        {
            string _now = CommonShared.GetMyDateTime();
            record.Modification = _now;
            await _context.Positions.AddAsync(record);

            // update db required to get lastid
            await _context.SaveChangesAsync();

            // create tppblock if not exists
            Tppblock? tppblock = await this.GetTppBlockAsync(record.Tppid, record.Tppblocksec);

            if (tppblock is null)
            {
                Tppblock newtppblock = new Tppblock
                {
                    Tppid = record.Tppid,
                    Tppblocksec = record.Tppblocksec,
                    Creation = _now,
                    Modification = _now
                };
                await this.CreateTppBlockAsync(newtppblock);
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
            await this.CreateTppBlockSecuenceAsync(tppblocksecuence);

            // update db
            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<Position?> UpdateAsync(Position updaterecord, int id)
        {
            var record = await this.GetAsync(id);
            if (record is null) return null;

            record.Sessionid = updaterecord.Sessionid;
            record.Guid = updaterecord.Guid;
            record.Tppid = updaterecord.Tppid;
            record.Tppcheck = updaterecord.Tppcheck;
            record.Tppblocksec = updaterecord.Tppblocksec;
            record.Sec = updaterecord.Sec;
            //record.Creation = updaterecord.Creation;
            record.Modification = CommonShared.GetMyDateTime();
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

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Position?> DeleteAsync(int id)
        {
            var record = await this.GetAsync(id);
            if (record is null || record.Deleted == 1) return null;

            record.Deleted = 1;
            record.Modification = CommonShared.GetMyDateTime();

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Tppblock?> GetTppBlockAsync(int tppid, int tppblocksec)
        {
            return await _context.TppBlocks
                                 .Where(a => a.Tppid == tppid && a.Tppblocksec == tppblocksec)
                                 .FirstOrDefaultAsync();
        }

        public async Task CreateTppBlockAsync(Tppblock tppblock)
        {
            await _context.TppBlocks.AddAsync(tppblock);
        }

        public async Task CreateTppBlockSecuenceAsync(Tppblocksecuence tppblocksecuence)
        {
            await _context.TppBlockSecuences.AddAsync(tppblocksecuence);
        }
    }
}
