using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class Detail
    {
        [Key]
        public int IntAreaID { get; set; }

        public int IntProcessID { get; set; }

        public int IntCharacteristicID { get; set; }

        public int IntInspPlanID { get; set; }

        public int IntProductNameID { get; set; }

        public int IntInspPlanDataID { get; set; }

        public int IntProductionDataID { get; set; }

        public int IntLineID { get; set; }

        public int IntJobID { get; set; }

        [StringLength(100)]
        public string? StrProcessLineCode { get; set; }

        [StringLength(100)]
        public string? StrProcessLineName { get; set; }

        [StringLength(100)]
        public string? StrNameArea { get; set; }


        [StringLength(100)]
        public string? StrProcessName { get; set; }

        [StringLength(200)]
        public string? StrCharacteristicName { get; set; }

        public int? IntMeasureTypeID { get; set; }

        [StringLength(100)]
        public string? StrMeaType { get; set; }


        [StringLength(100)]
        public string? StrInspPlanName { get; set; }


        [StringLength(100)]
        public string? StrNameProduct { get; set; }

        [StringLength(100)]
        public string? StrModelInternal { get; set; }

        public int IntPlanTypeID { get; set; }

        public int IntDecimals { get; set; }

        [StringLength(100)]
        public string? StrJobCode { get; set; }

        [StringLength(100)]
        public string? StrPlanTypeName { get; set; }

        [StringLength(100)]
        public string? StrPOCode { get; set; }

        public DateTime? DtStartTime { get; set; }

        public DateTime? DtEndTime { get; set; }

        public double? FtLCL { get; set; }

        public double? FtUCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCLS3 { get; set; }

        public double? FtMean { get; set; }

        public int? IntQuantity { get; set; }

        public int? IntLSpecWarming { get; set; }

        public int? IntHSpecWarming { get; set; }

        public double? FtAvg { get; set; }
    }
}
