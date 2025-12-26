using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlanTracking")]
    public class InspectionPlanTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intInspPlanID")]
        public int? IntInspPlanID { get; set; }

        [Column("intCharacteristicID")]
        public int? IntPlanTypeID { get; set; }

        [Column("intPlanState")]
        public int? IntPlanState { get; set; }

        [Column("dtCreateTime", TypeName = "datetime")]
        public DateTime? DtUpdateTime { get; set; }

        [Column("intUserID")]
        public int? IntUserID { get; set; } 
    }
}
