using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbTVDisplay")]
    public class TVDisplay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(50)]
        [Column("strTVName", TypeName = "nvarchar(50)")]
        public string? StrTVName { get; set; }

        [MaxLength(50)]
        [Column("strProductionID", TypeName = "nvarchar(50)")]
        public string? StrProductionID { get; set; }

        [MaxLength(50)]
        [Column("strCharacteristicID", TypeName = "nvarchar(50)")]
        public string? StrCharacteristicID { get; set; }

        [MaxLength(50)]
        [Column("strPlanTypeID", TypeName = "nvarchar(50)")]
        public string? StrPlanTypeID { get; set; }

        [MaxLength(50)]
        [Column("strMoldID", TypeName = "nvarchar(50)")]
        public string? StrMoldID { get; set; }

        [MaxLength(50)]
        [Column("strCavityID", TypeName = "nvarchar(50)")]
        public string? StrCavityID { get; set; }
    }
}
