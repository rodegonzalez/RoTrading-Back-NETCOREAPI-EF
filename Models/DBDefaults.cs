namespace GeneralStore.Models 
{

    public class Market
    {
        public int Id { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Diary
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Divisa
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Position_pattern 
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Position_highpattern
    {
        public int Id { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Position_setup
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
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
    public class Session
    {
        public int Id { get; set; }
        public float? Usdeur { get; set; }
        public int? Haspositions { get; set; }
        public int? Consolidated { get; set; }
        public string? Note { get; set; }
    }

}