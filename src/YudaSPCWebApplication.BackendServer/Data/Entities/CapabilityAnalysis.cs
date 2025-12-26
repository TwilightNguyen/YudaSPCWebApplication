using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbCapabilityAnalysis")]
    public class CapabilityAnalysis
    {
        [Key]
        [Column("intJobID", Order = 0)]
        public int IntJobID { get; set; }

        [Key]
        [Column("intPlanTypeID", Order = 1)]
        public int IntPlanTypeID { get; set; }

        [Key]
        [Column("intCharacteristicID", Order = 2)]
        public int IntCharacteristicID { get; set; }

        [Column("intProductedQty")]
        public int? IntProductedQty { get; set; }

        [Column("ftAverage")]
        public float? FtAverage { get; set; }

        [Column("ftStDevWithin")]
        public float? FtStDevWithin { get; set; }

        [Column("ftStDevOverall")]
        public float? FtStDevOverall { get; set; }
    }
}
