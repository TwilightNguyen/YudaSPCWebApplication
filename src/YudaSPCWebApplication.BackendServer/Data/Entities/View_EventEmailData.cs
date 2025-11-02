using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class View_EventEmailData
    {
        [Key]
        public int IntID { get; set; }

        public int? IntEventRoleID { get; set; }

        public int? IntProductionID { get; set; }

        public int? IntOutSpec { get; set; }

        public int? IntQty { get; set; }

        public int? IntDefectRate { get; set; }

        public int? IntAEActive { get; set; }

        public int? IntEventSent { get; set; }

        public int? IntEventSentFail { get; set; }

        public DateTime? DtUpdateTime { get; set; }

        public DateTime? DtEmailSentTime { get; set; }

        public int? IntAreaID { get; set; }

        [MaxLength(100)]
        public string? StrNameArea { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(100)]
        public string? StrProcessName { get; set; }

        public int? IntCharacteristicID { get; set; }

        [MaxLength(200)]
        public string? StrCharacteristicName { get; set; }

        public int? IntLineID { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineCode { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineName { get; set; }
    }
}
