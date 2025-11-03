using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class View_ProductionData
    {
        [Key]
        public int IntAreaID { get; set; }

        public int? IntID { get; set; }

        public int? IntLineID { get; set; }

        [MaxLength(100)]
        public string? StrNotes { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineCode { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineName { get; set; }

        public int? IntJobID { get; set; }

        public int? IntUserID { get; set; }

        public DateTime? DtStartTime { get; set; }

        public DateTime? DtEndTime { get; set; }

        [MaxLength(100)]
        public string? StrJobCode { get; set; }

        [MaxLength(100)]
        public string? StrSOCode { get; set; }

        [MaxLength(100)]
        public string? StrPOCode { get; set; }

        public int? IntJobQty { get; set; }

        public int? IntOutputQty { get; set; }

        public int? IntProductID { get; set; }

        public DateTime? DtJobCreate { get; set; }

        public int? IntJobDecisionID { get; set; }

        [MaxLength(100)]
        public string? StrNameProduct { get; set; }

        public int? IntColorCode { get; set; }

        [MaxLength(100)]
        public string? StrDecision { get; set; }

        [MaxLength(100)]
        public string? StrModelInternal { get; set; }

        public int? IntInspPlanID { get; set; }

        [MaxLength(100)]
        public string? StrNameArea { get; set; }

        [MaxLength(100)]
        public string? StrInspPlanName { get; set; }

        public int? IntPlanTypeID { get; set; }
        public int? intInspPlanDataID { get; set; }

        [MaxLength(100)]
        public string? StrPlanTypeName { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(100)]
        public string? StrProcessName { get; set; }

        public int? IntCharacteristicID { get; set; }

        [MaxLength(200)]
        public string? StrCharacteristicName { get; set; }

        [MaxLength(100)]
        public string? StrCharacteristicUnit { get; set; }

        public int? IntCharacteristicType { get; set; }

        public int? IntCharacteristicOrder { get; set; }

        public int? IntMeaTypeID { get; set; }

        [MaxLength(100)]
        public string? StrMeaType { get; set; }

        public double? FtUCL { get; set; }

        public double? FtLCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCLS3 { get; set; }

        public bool? BoolCpkControl { get; set; }

        public double? FtCpkMax { get; set; }

        public double? FtCpkMin { get; set; }

        public bool? BoolDataEntry { get; set; }

        public bool? BoolSPCChart { get; set; }

        public bool? BoolDeleted { get; set; }

        public int? IntDecimals { get; set; }

        [MaxLength(20)]
        public string? StrSampleSize { get; set; }

        [MaxLength(100)]
        public string? StrCustomerName { get; set; }

        public DateTime? DtProductionDate { get; set; }

        public int? IntProductionQty { get; set; }

        [MaxLength(100)]
        public string? StrLotInform { get; set; }

        [MaxLength(50)]
        public string? StrMaterialInform { get; set; }

        [MaxLength(200)]
        public string? StrFullName { get; set; }

        [MaxLength(100)]
        public string? StrDescription { get; set; }

        public int? IntMoldQty { get; set; }

        public int? IntCavityQty { get; set; }
    }
}
