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
}