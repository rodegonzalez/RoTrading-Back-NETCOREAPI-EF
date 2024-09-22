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

        /*
         * str = xml with the search to get 
         */
        //public async Task<DataTable?> GetPositionsSearchAsync(SearchOptions searchOptions)
        public async Task<DataTable?> GetPositionsSearchAsync(string? _searchOptions)
        {
            try
            {
                if (_searchOptions == null) return null;

                var searchOptions = JsonSerializer.Deserialize<SearchOptions>(_searchOptions);

                var query = _context.PositionViews.AsQueryable();
                if (searchOptions.Temporality != null)
                {
                    query = query.Where(p => p.Setup1id.ToLower() == searchOptions.Temporality.ToLower());
                }
                if (searchOptions.Buysell != null)
                {
                    query = query.Where(p => p.Buysell.ToLower() == searchOptions.Buysell.ToLower());
                }

                query = query.Where(p => p.Status.ToLower() == "closed" && p.Deleted == 0);
                var positions = await query.ToListAsync();

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
                    button = $"<button id='button{p.Id}' class='btn btn-success' (click)='verID({p.Id})'>Ver</button>",
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

        private string processSearch(string str)
        {
            return str;

            // Crear un diccionario para almacenar los pares nombre:valor
            var options = new Dictionary<string, string>();

            // Cargar el XML en un objeto XmlDocument
            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(str);

            // Extraer los pares nombre:valor del XML
            foreach (System.Xml.XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if (node is System.Xml.XmlElement element)
                {
                    string name = element.Name;
                    string value = element.InnerText;
                    options[name] = value;
                }
            }

            // Procesar los pares nombre:valor (aquí puedes agregar tu lógica de procesamiento)
            var processedOptions = options.Select(kv => new
            {
                Key = kv.Key,
                Value = kv.Value
            }).ToArray();
            //return processedOptions;

            // Convertir el array de objetos procesados a JSON
            string? jsonOptions = JsonSerializer.Serialize(processedOptions);
            return jsonOptions;
        }

    } // end class
} // end namespace
