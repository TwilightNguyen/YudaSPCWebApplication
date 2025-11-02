using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YudaSPCWebApplication.BackendServer.Data.Entities.Interfaces;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventEmailHistory")]
    public class EventEmailHistory 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int? IntAreaID { get; set; }

        public int? IntCharacteristicID { get; set; }

        public int? IntProductionID { get; set; }

        public int? IntProcessID { get; set; }

        public int? IntLineID { get; set; }

        public int? IntDefectRate { get; set; }

        public int? IntEmailEventModel { get; set; }

        public int? IntAEActive { get; set; }

        public DateTime? DtTimeIn { get; set; }

        [MaxLength(300)]
        [Column(TypeName = "nvarchar(300)")]
        public string? StrEmailAddress { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessName { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? StrCharacteristicName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessLineCode { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessLineName { get; set; }
    }
}
