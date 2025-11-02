using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbJobDecision")]
    public class JobDecision
    {
        [Key]
        public int IntID { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? StrDecision { get; set; }

        public int IntColorCode { get; set; }
    }
}
