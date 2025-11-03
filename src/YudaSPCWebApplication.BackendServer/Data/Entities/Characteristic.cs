using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbBlockVialFixture")]
    public class Characteristic 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int? IntAreaID { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? StrCharacteristicName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrNameArea { get; set; }

        public int? IntMeaTypeID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrMeaType { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessName { get; set; }

        public int? IntCharacteristicType { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        public string? StrCharacteristicUnit { get; set; }

        public bool? BoolDeleted { get; set; }

        public int? IntDefectRateLimit { get; set; }

        public int? IntEventEnable { get; set; }

        public int? IntEmailEventModel { get; set; }

        public int? IntDecimals { get; set; }
    }
}
    