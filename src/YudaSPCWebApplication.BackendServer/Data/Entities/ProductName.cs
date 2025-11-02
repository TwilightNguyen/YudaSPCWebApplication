using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProductName")]
    public class ProductName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameProduct { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrModelInternal { get; set; }

        public int? IntAreaID { get; set; }

        public int? IntInspPlanID { get; set; }

        public bool BoolDeleted { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrCustomerName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNotes { get; set; }

        public int? IntVialFixture { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrVialFixture { get; set; }
    }
}
