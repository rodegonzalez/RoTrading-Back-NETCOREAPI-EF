namespace GeneralStore.Models 
{
    public class Ticker
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Tictype { get; set; }
        public int? Active { get; set; }
        public float? Tickmin { get; set; }
        public float? Tickminvalue { get; set; }
        public int? Divisaid { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Tickeraccount
    {
        public int Tickerid { get; set; }
        public int Accountid { get; set; }
        public string? Creation { get; set; }        
        public string? Modification { get; set; }
        public float? Commission { get; set; }
    }

}