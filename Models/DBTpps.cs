using Microsoft.AspNetCore.Identity;

namespace GeneralStore.Models 
{
    public class Tpp
    {
        public int Id { get; set; }        
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Blockprefix { get; set; }
        public int? Maxblocksecuence { get; set; }
        public string? Status { get; set; }
        public int? Active { get; set; }
        public int? Deleted { get; set; }
        public string? Note { get; set; }
    }
    public class Tppblock
    {
        public int Id { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public int? Tppid { get; set; }
        public int? Tppblocksec { get; set; }
    }
    public class Tppblocksecuence
    {
        public int Id { get; set; }
        public string? Creation { get; set; }
        public string? Modification { get; set; }
        public int? Tppid { get; set; }
        public int? Tppblocksec { get; set; }
        public string? Sessionid { get; set; }
        public int? Positionid { get; set; }
        public int? Sec { get; set; }
    }

} 