namespace GeneralStore.Models 
{
    public class PositionView
    {
        public int Id { get; set; }
        public string? Block { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
        public string? Guid { get; set; }
        public string? Datetimein { get; set; }
        public string? Datetimeout { get; set; }
        public string? Buysell { get; set; }
        public decimal? Pricein { get; set; }
        public decimal? Priceout { get; set; }
        public int? Ticks { get; set; }
        public decimal? Profit { get; set; }
        public decimal ? Stoploss { get; set; }
        public int? Contracts { get; set; }
        public decimal? Commision { get; set; }
        public decimal? Euros { get; set; }
        public decimal? Dollareuro { get; set; }
        public string? Imagepath { get; set; }
        public int? Divisaid { get; set; }
        public int? Accountid { get; set; }
        public int? Marketid { get; set; }
        public int? Tppid { get; set; }
        public string? Status { get; set; }
        public int? Patternid { get; set; }
        public int? Setupid { get; set; }
        public int? Tickerid { get; set; }

        public int? Tppcheck { get; set; }  // 1- en tpp, 0- no tpp
        public int? Isrealcheck { get; set; }  // 1- real, 0- training account

        public int? Processed { get; set; }
        public string? Acctype { get; set; }
        public string? Temporal { get; set; }

        public string? Divisa { get; set; }
        public string? Account { get; set; }
        public string? Market { get; set; }
        public string? Pattern { get; set; }
        public string? Tpp { get; set; }
        public string? Setup { get; set; }
        public string? Ticker { get; set; }
        public string? Broker { get; set; }


    }
}