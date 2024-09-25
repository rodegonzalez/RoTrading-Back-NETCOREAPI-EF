using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralStore.Models 
{
    public class DataTable
    {
        [NotMapped]
        public object[]? tableColumns { get; set; }

        [NotMapped]
        public object[]? tableData { get; set; }
        [NotMapped]
        public object? summarize { get; set; }
    }

    /*
    public class Summarize
    {
        [NotMapped]
        public object? positionsData_operations { get; set; }

        [NotMapped]
        public object? positionsData_blocks { get; set; }

        [NotMapped]
        public object? positionsData_ticks { get; set; }
        [NotMapped]
        public object? positionsData_posneg { get; set; }

    }
    */

    // Opciones dentro del form del buscador
    public class SearchOptions
    {
        public string? Sessionid { get; set; }
        public int? Accountid { get; set; }
        public int? Tickerid { get; set; }
        public int? Pattern1id { get; set; }
        public string? Pattern2id { get; set; }
        public string? Setup1id { get; set; }
        public int? Setup2id { get; set; }
        public string? Buysell { get; set; }
    }
}
