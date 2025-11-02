using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlanTracking")]
    public class InspectionPlanTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int? IntAreaID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }

        public int? IntInspPlanID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrInspPlanName { get; set; }

        public int? IntPlanTypeID { get; set; }

        [MaxLength(25)]
        [Column(TypeName = "nvarchar(25)")]
        public string? StrPlanTypeName { get; set; }

        public int? IntPlanState { get; set; }

        public DateTime? DtUpdateTime { get; set; }

        public int? IntUserID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrFullName { get; set; }
    }
}
