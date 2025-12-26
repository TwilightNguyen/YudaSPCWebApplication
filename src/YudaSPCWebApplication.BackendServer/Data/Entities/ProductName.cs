using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProductName")]
    public class ProductName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strNameProduct", TypeName = "nvarchar(100)")]
        public string? StrNameProduct { get; set; }

        [Column("intAreaID")]
        public int? IntAreaID { get; set; }


        [Column("intInspPlanID")]
        public int? IntInspPlanID { get; set; }


        [Column("boolDeleted")]
        public bool BoolDeleted { get; set; }

        [MaxLength(100)]
        [Column("strModelInternal", TypeName = "nvarchar(100)")]
        public string? StrModelInternal { get; set; }

        [MaxLength(100)]
        [Column("strCustomerName", TypeName = "nvarchar(100)")]
        public string? StrCustomerName { get; set; }

        [MaxLength(100)]
        [Column("strNotes", TypeName = "nvarchar(100)")]
        public string? StrNotes { get; set; }

        [Column("intVialFixture")]
        public int? IntVialFixture { get; set; }

        [MaxLength(100)]
        [Column("strDescription", TypeName = "nvarchar(100)")]
        public string? StrDescription { get; set; }

        [Column("intMoldQty")]
        public int? IntMoldQty { get; set; }

        [Column("intCavityQty")]
        public int? IntCavityQty { get; set; }
    }
}
