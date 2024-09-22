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
        public string? Datemin { get; set; }
        public string? Datemax { get; set; }
        public string? Dateyear { get; set; }
        public string? Temporality { get; set; }
        public string? Pattern1id { get; set; }
        public int? Setup2id { get; set; }
        public string? Buysell { get; set; }
    }
}
