using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbJobData")]
    public class JobData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intAreaID")]
        public int? IntAreaID { get; set; }

        [Column("intProductID")]
        public int? IntProductID { get; set; }

        [MaxLength(100)]
        [Column("strJobCode", TypeName = "nvarchar(100)")]
        public string? StrJobCode { get; set; }

        [MaxLength(100)]
        [Column("strPOCode", TypeName = "nvarchar(100)")]
        public string? StrPOCode { get; set; }

        [MaxLength(100)]
        [Column("strSOCode", TypeName = "nvarchar(100)")]
        public string? StrSOCode { get; set; }

        [Column("intJobQty")]
        public int? IntJobQty { get; set; }

        [Column("intOutputQty")]
        public int? IntOutputQty { get; set; }

        [Column("dtCreateTime", TypeName = "datetime")]
        public DateTime? DtCreateTime { get; set; }

        [Column("intJobDecisionID")]
        public int? IntJobDecisionID { get; set; }

        [Column("intUserID")]
        public int? IntUserID { get; set; }

        [Column("boolDeleted")]
        public bool? BoolDeleted { get; set; }
    }
}
