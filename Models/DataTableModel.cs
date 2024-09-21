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

}
