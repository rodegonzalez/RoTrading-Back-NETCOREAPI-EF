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

    public class JsonRequest
    {
        // Contenedor de opciones
        public string? Options { get; set; }
    }

    // Opciones dentro del contenedor
    public class SearchOptions
    {
        public string? Datemin { get; set; } = null;
        public string? Datemax { get; set; } = null;
        public string? Dateyear { get; set; } = null;
        public string? Temporality { get; set; } = null;
        public string? Pattern1id { get; set; } = null;
        public string? Setup2id { get; set; } = null;
        public string? Buysell { get; set; } = null;


    }


}
