using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlan")]
    public class InspectionPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrInspPlanName { get; set; }

        public int? IntAreaID { get; set; }

        public DateTime? DtCreateTime { get; set; }

        public DateTime? DtUpdateTime { get; set; }

        public bool? BoolDeleted { get; set; }
    }
}
