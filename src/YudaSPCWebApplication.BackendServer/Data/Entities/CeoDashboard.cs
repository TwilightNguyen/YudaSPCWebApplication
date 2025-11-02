using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class CeoDashboard
    {
        [Key]
        public int IntInspPlanID { get; set; }

        public int IntPlanTypeID { get; set; }

        public int IntCharacteristicID { get; set; }

        [StringLength(100)]
        public string? VarCharacteristicValue { get; set; }

        public double? FtLCL { get; set; }

        public double? FtUCL { get; set; }

        public DateTime DtTimeStamp { get; set; }
    }
}
