namespace GeneralStore.Models 
{
    public class Session
    {
        public string Id { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public float? Usdeur { get; set; }
        public int? Haspositions { get; set; }
        public int? Consolidated { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Sessionnote { get; set; }
    }

}