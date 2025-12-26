using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProcessLine")]
    public class ProcessLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strProcessLineName", TypeName = "nvarchar(100)")]
        public string? StrProcessLineName { get; set; }

        [MaxLength(100)]
        [Column("strProcessLineCode", TypeName = "nvarchar(100)")]
        public string? StrProcessLineCode { get; set; }

        [Column("intAreaID")]
        public int? IntProcessID { get; set; }
    }
}
