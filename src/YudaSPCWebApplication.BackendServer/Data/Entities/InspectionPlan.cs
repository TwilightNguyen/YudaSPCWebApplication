using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlan")]
    public class InspectionPlan
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strInspPlanName", TypeName = "nvarchar(100)")]
        public string? StrInspPlanName { get; set; }

        [Column("intAreaID")]
        public int? IntAreaID { get; set; }

        [Column("dtCreateTime", TypeName = "datetime")]
        public DateTime? DtCreateTime { get; set; }

        [Column("dtUpdateTime", TypeName = "datetime")]
        public DateTime? DtUpdateTime { get; set; }

        [Column("boolDeleted")]
        public bool? BoolDeleted { get; set; }
    }
}
