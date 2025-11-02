using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbBlockVialFixture")]
    public class BlockVialFixture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrVialFixture { get; set; }
    }
}
