using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;
using System.Collections.Generic;
using GeneralStore.Repositories;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Json.Nodes;

namespace GeneralStore.Repositories
{
    public class ReportRepository : IDataTable
    {
        private readonly Db _context;

        public ReportRepository(Db context)
        {
            _context = context;
        }

        public DataTable? GetTest()
        {
            var tableColumns = new List<object>
            {
                new { title = "name", data = "name" },
                new { title = "position", data = "position" },
                new { title = "office", data = "office" }
            };

            var tableData = new List<object>
            {
                new { name = "Pantera Nixon", position = "System Admin", office = "Las Palmas" },
                new { name = "Bear Winters", position = "Astronauta", office = "Madrid" }
            };

            DataTable _datatable = new DataTable()
            {
                tableColumns = tableColumns.ToArray(),
                tableData = tableData.ToArray()
            };

            return _datatable;
        }

        public async Task<DataTable?> GetPositionsAsync()
        {
            try
            {                
                var tableColumns = new List<object>
                {
                    new { title = "id", data = "id" },
                    new { title = "sessionid", data = "sessionid" },
                    new { title = "account", data = "account" },
                    new { title = "ticker", data = "ticker" },
                    new { title = "tpp", data = "tpp" },
                    new { title = "pattern", data = "pattern" },
                    new { title = "temporality", data = "temporality" },
                    new { title = "setup", data = "setup" },
                    new { title = "button", data = "button" },
                };

                var positions = await _context.PositionViews.ToListAsync();
                var tableData = positions.Select(p => new
                {
                    id = p.Id,
                    sessionid = p.Sessionid,
                    account = p.Account,
                    ticker = p.Ticker,
                    tpp = p.Tpp,
                    pattern = p.Pattern,
                    temporality = p.Setup1id,
                    setup = p.Setup,
                    button = $"<button id='button{p.Id}' class='btn btn-success'>Ver</button>",
                }).ToArray();
                
                DataTable _datatable = new DataTable()
                {
                    tableColumns = tableColumns.ToArray(),
                    tableData = tableData.ToArray()
                };

                return _datatable;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<DataTable?> GetPositionsSearchAsync(string? _searchOptions)
        {
            try
            {
                if (_searchOptions is null) return null;

                // query
                var query = this.prepareQuery(_searchOptions);
                var positions = await query.ToListAsync();

                // create structures
                var tableColumns = new List<object>
                {
                    new { title = "Id", data = "id" },
                    new { title = "Sessionid", data = "sessionid" },
                    new { title = "Account", data = "account" },
                    new { title = "Ticker", data = "ticker" },
                    new { title = "Tpp", data = "tpp" },
                     new { title = "BuySell", data = "buysell" },
                    new { title = "HPattern", data = "hpattern" },
                    new { title = "Pattern", data = "pattern" },
                    new { title = "Setup", data = "setup" },
                    new { title = "Commission", data = "commission" },
                    new { title = "Ticks", data = "ticks" },
                    new { title = "Button", data = "button" },
                };

                var tableData = positions.Select(p => new
                {
                    id = p.Id,
                    sessionid = p.Sessionid,
                    account = p.Account,
                    ticker = p.Ticker,
                    tpp = p.Tpp + "[" + p.Tppblocksec + "-" + p.Sec + "]",
                    buysell = p.Buysell,
                    hpattern = p.Pattern,
                    pattern = p.Pattern2id,
                    setup = p.Setup1id + ": " + p.Setup,
                    commission = p.Commission,
                    ticks = p.Opresultticks,
                    button = $"<button id='button{p.Id}' class='btn btn-success'>Ver</button>",
                }).ToArray();

                // sumarize            
                var chartLabels = positions.Select(p => p.Id.ToString()).ToList();
                var chartData = positions.Select(p => p.Opresultticks ?? 0).ToList();
                var _summarize = new
                {
                    positionsData_operations = new
                    {
                        chartLabels = chartLabels,
                        chartData = chartData
                    }
                };

                // return structures
                DataTable _datatable = new DataTable()
                {
                    tableColumns = tableColumns.ToArray(),
                    tableData = tableData.ToArray(),
                    summarize = _summarize
                };

                return _datatable;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        
        }
        private IQueryable<PositionView> prepareQuery(string? _searchOptions)
        {
            var query = _context.PositionViews.AsQueryable();

            //var query = _query;
            var searchOptions = JsonSerializer.Deserialize<SearchOptions>(_searchOptions ?? string.Empty);

            //// sessID from
            //if (!string.IsNullOrEmpty(searchOptions.Sessionidfrom) && searchOptions.Sessionidfrom != "not-set")
            //{
            //    searchOptions.Sessionidfrom = CommonShared.GetMySessionidFormatted(searchOptions.Sessionidfrom);
            //    //query = query.Where(p => p.Sessionid >= searchOptions.Sessionidfrom);
            //    query = query.Where(p => int.Parse(p.Sessionid) >= int.Parse(searchOptions.Sessionidfrom));
            //}

            //// sessID to
            //if (!string.IsNullOrEmpty(searchOptions.Sessionidto) && searchOptions.Sessionidto != "not-set")
            //{
            //    searchOptions.Sessionidto = CommonShared.GetMySessionidFormatted(searchOptions.Sessionidto);
            //    //query = query.Where(p => p.Sessionid < searchOptions.Sessionidto);
            //    query = query.Where(p => DateTime.ParseExact(p.Sessionid, CommonShared.GetMySessionidFormat(), null) < DateTime.ParseExact(searchOptions.Sessionidto, CommonShared.GetMySessionidFormat(), null));
            //}

            // buysell
            if (!string.IsNullOrEmpty(searchOptions.Buysell) && searchOptions.Buysell != "not-set")
            {
                query = query.Where(p => p.Buysell.ToLower() == searchOptions.Buysell.ToLower());
            }
            // winloss
            if (!string.IsNullOrEmpty(searchOptions.WinLoss) && searchOptions.WinLoss != "not-set")
            {
                query = (string.Equals(searchOptions.WinLoss.ToLower(), "win")) ? query.Where(p => p.Opresultticks >= 0) : query.Where(p => p.Opresultticks < 0);
            }
            // account and ticker
            if (searchOptions.Accountid > 0)
            {
                query = query.Where(p => p.Accountid == searchOptions.Accountid);
            }
            if (searchOptions.Tickerid > 0)
            {
                query = query.Where(p => p.Tickerid == searchOptions.Tickerid);
            }
            // pattern
            if (searchOptions.Pattern1id > 0)
            {
                query = query.Where(p => p.Pattern1id == searchOptions.Pattern1id);
            }
            if (!string.IsNullOrEmpty(searchOptions.Pattern2id) && searchOptions.Pattern2id != "not-set")
            {
                query = query.Where(p => p.Pattern2id.ToLower() == searchOptions.Pattern2id.ToLower());
            }
            // setup
            if (!string.IsNullOrEmpty(searchOptions.Setup1id) && searchOptions.Setup1id != "not-set")
            {
                query = query.Where(p => p.Setup1id.ToLower() == searchOptions.Setup1id.ToLower());
            }
            if (searchOptions.Setup2id > 0)
            {
                query = query.Where(p => p.Setup2id == searchOptions.Pattern1id);
            }
            // finally: add only closed and not deleted items.
            // TODO: review only for consolidated positions.
            query = query.Where(p => p.Status.ToLower() == "closed" && p.Deleted == 0);

            return query;
        }

    } // end class
} // end namespace
