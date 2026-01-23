using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlanSub")]
    public class InspectionPlanSub
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [Column("intInspPlanID")]
        public int IntInspPlanID { get; set; }

        [Column("intPlanTypeID")]
        public int IntPlanTypeID { get; set; }

        [Column("boolDeleted")]
        public bool BoolDeleted { get; set; } = false;

        [Column("dtCreateTime", TypeName = "datetime")]
        public DateTime DtCreateTime { get; set; } = DateTime.Now;
    }
}
