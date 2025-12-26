using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbJobDecision")]
    public class JobDecision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(20)]
        [Column("strDecision", TypeName = "nvarchar(20)")]
        public string? StrDecision { get; set; }

        [Column("intColorCode")]
        public int IntColorCode { get; set; }
    }
}
