using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProductionArea")]
    public class ProductionArea 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strNameArea", TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }
    }
}
