using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YudaSPCWebApplication.BackendServer.Data.Entities.Interfaces;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProductionArea")]
    public class ProductionArea 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }
    }
}
