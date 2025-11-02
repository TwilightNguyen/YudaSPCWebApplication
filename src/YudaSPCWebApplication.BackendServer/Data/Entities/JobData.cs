using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbJobData")]
    public class JobData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrJobCode { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrPOCode { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameProduct { get; set; }

        public int? IntInspPlanID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrInspPlanName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrModelInternal { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrStatus { get; set; }

        public int? IntJobQty { get; set; }

        public int? IntOutputQty { get; set; }

        public int? IntProductID { get; set; }

        public int? IntAreaID { get; set; }

        public DateTime? DtCreateTime { get; set; }

        public bool? BoolDeleted { get; set; }

        public int? IntJobDecisionID { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? StrDecision { get; set; }

        public int? IntColorCode { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrSOCode { get; set; }

        public int? IntVialFixture { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrVialFixture { get; set; }
    }
}
