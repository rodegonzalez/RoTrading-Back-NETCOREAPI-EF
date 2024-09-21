using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Common;
using System.Collections.Generic;
using GeneralStore.Repositories;
using System.Text.Json;

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
                    setup = p.Setup,
                    button =  $"<button id='button{p.Id}' class='btn btn-success' (click)='verID({p.Id})'>Ver</button>",
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
    }
}
