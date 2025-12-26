using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProcess")]
    public class Process
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strProcessName", TypeName = "nvarchar(100)")]
        public string? StrProcessName { get; set; }

        [Column("intAreaID")]
        public int? IntAreaID { get; set; }
    }
}
