namespace GeneralStore.Models 
{
    public class Position
    {
        public int Id { get; set; }
        public required string Sessionid { get; set; } // in yyyyMMdd format
        public required string Guid { get; set; }
        public required int Tppid { get; set; }
        public required int Tppcheck { get; set; }  // 1- en tpp, 0- no tpp
        public required int Tppblocksec { get; set; }
        public required int Sec { get; set; }  // secuence into block
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Timein { get; set; }
        public string? Timeout { get; set; }
        public decimal? Pricein { get; set; }
        public decimal? Priceout { get; set; }
        public string? Buysell { get; set; }
        public int? Contracts { get; set; }
        public int? Opresultticks { get; set; }
        public decimal? Opresult { get; set; } // in divisaid
        public decimal? Commission { get; set; } // in divisaid
        public decimal? Opresulteur { get; set; }
        public decimal? Usdeur { get; set; }
        public int? Divisaid { get; set; }
        public int? Accountid { get; set; } // account & broker
        public int? Tickerid { get; set; } // ticker & market 
        public int? Pattern1id { get; set; }
        public string? Pattern2id { get; set; }
        public string? Setup1id { get; set; }
        public int? Setup2id { get; set; }
        public int? Processed { get; set; }
        public int? Deleted { get; set; }
        public string? Deletednote { get; set; }
        public string? Imagepath { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }

    }
}