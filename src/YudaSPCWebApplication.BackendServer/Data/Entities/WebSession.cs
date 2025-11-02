using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbWebSession")]
    public class WebSession
    {
        [Key]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrSessionID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrIpAddress { get; set; }

        public DateTime? DtStartTime { get; set; }

        public DateTime? DtEndTime { get; set; }
    }
}
