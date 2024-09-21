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

        public async Task<DataTable?> GetPositionsAsync()
        {
            //var positions = await _context.Positions.FindAsync();

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
    }
}
