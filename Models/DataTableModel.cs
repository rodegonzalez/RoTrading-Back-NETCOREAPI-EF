using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralStore.Models 
{
    public class DataTable
    {
        [NotMapped]
        public object[]? tableColumns { get; set; }

        [NotMapped]
        public object[]? tableData { get; set; }

    }

    // Opciones dentro del contenedor
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
