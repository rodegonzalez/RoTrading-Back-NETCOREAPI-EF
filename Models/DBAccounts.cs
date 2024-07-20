namespace GeneralStore.Models 
{
    public class Account
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Amount_initial { get; set; }
        public decimal? Amount_current { get; set; }
        public int? Brokerid { get; set; }
        public int? Divisaid { get; set; }
        public string? Status { get; set; }
        public string? Acctype { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
}