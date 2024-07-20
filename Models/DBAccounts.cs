using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralStore.Models 
{
    public class Account
    {
        [Key]
        public int Idaccount { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Idbroker { get; set; }
        public int? Iddivisa { get; set; }
        public string? Status { get; set; }
        public string? Acctype { get; set; }
        public int? active { get; set; }
        public int? deleted { get; set; }
        public string? note { get; set; }

        [Column("ammount_initial")] // Mapea esta propiedad a la columna 'ammount_initial' en SQLite
        public decimal? Ammount_initial { get; set; }

    }
}