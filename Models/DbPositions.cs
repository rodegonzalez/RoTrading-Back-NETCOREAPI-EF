namespace GeneralStore.Models 
{
    public class Position
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }

        public string? Guid { get; set; }
        public string? DateTimeIN { get; set; }
        public string? DateTimeOut { get; set; }
        public string? Buysell { get; set; }
        public decimal? PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public int? Ticks { get; set; }
        public int? Contracts { get; set; }
        public decimal? Commision { get; set; }
        public decimal? Euros { get; set; }
        public decimal? Dollareuro { get; set; }
        public string? Imagepath { get; set; }
        public int? DivisaId { get; set; }
        public int? AccountId { get; set; }
        public string? Status { get; set; }
        public int? PatternId { get; set; }
        public int? SetupId { get; set; }
        public int? TickerId { get; set; }
        public int? Processed { get; set; }
    }
}