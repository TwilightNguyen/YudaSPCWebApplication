using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbCharacteristic")]
    public class Characteristic 
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(200)]
        [Column("strCharacteristicName", TypeName = "nvarchar(200)")]
        public string? StrCharacteristicName { get; set; }

        [Column("intMeaTypeID")]
        public int? IntMeaTypeID { get; set; }

        [Column("intProcessID")]
        public int? IntProcessID { get; set; }

        [Column("intCharacteristicType")]
        public int? IntCharacteristicType { get; set; }

        [MaxLength(10)]
        [Column("strCharacteristicUnit", TypeName = "nvarchar(10)")]
        public string? StrCharacteristicUnit { get; set; }

        [Column("boolDeleted")]
        public bool? BoolDeleted { get; set; }

        [Column("intDefectRateLimit")]
        public int? IntDefectRateLimit { get; set; }

        [Column("intEventEnable")]
        public int? IntEventEnable { get; set; }

        [Column("intEmailEventModel")]
        public int? IntEmailEventModel { get; set; }

        [Column("intDecimals")]
        public int? IntDecimals { get; set; }
    }
}
    