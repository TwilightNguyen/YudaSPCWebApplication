using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventRoles")]
    public class EventRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrRoleID { get; set; }

        public int? IntAreaID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessName { get; set; }

        public int? IntCharacteristicID { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? StrCharacteristicName { get; set; }

        public int? IntDefectRateLimit { get; set; }

        public int? IntEventEnable { get; set; }

        public int? IntMeaTypeID { get; set; }

        public int? IntEmailEventModel { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrMeaType { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrCharacteristicUnit { get; set; }
    }
}
