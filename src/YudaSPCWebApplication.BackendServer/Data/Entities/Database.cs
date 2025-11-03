using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class Database
    {
        [Key]
        public int IntAreaID { get; set; }

        public int? IntProcessID { get; set; }

        public int? IntCharacteristicID { get; set; }

        public int IntInspPlanID { get; set; }

        public int IntProductNameID { get; set; }

        public DateTime? DtCreateTime { get; set; }

        public DateTime? DtUpdateTime { get; set; }

        public int? IntInspPlanDataID { get; set; }

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

        public double? FtLCL { get; set; }

        public double? FtUCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCLS3 { get; set; }

        public double? Mean { get; set; }

        public bool? BoolSPCChart { get; set; }

        public bool? BoolDataEntry { get; set; }

        public int? IntPlanTypeID { get; set; }

        [StringLength(100)]
        public string? StrPlanTypeName { get; set; }

        [StringLength(100)]
        public string? StrCustomerName { get; set; }

        [StringLength(100)]
        public string? StrNotes { get; set; }

        public int? IntVialFixture { get; set; }

        [StringLength(50)]
        public string? StrVialFixture { get; set; }

        [StringLength(100)]
        public string? StrDescription { get; set; }

        public int? IntMoldQty { get; set; }

        public int? IntCavityQty { get; set; }
    }
}
