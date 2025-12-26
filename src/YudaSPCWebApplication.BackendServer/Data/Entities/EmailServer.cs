using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEmailServer")]
    public class EmailServer
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strEmailAddress", TypeName = "nvarchar(100)")]
        public string? StrEmailAddress { get; set; }

        [MaxLength(100)]
        [Column("strDisplayName", TypeName = "nvarchar(100)")]
        public string? StrDisplayName { get; set; }

        [MaxLength(100)]
        [Column("strPassword", TypeName = "nvarchar(100)")]
        public string? StrPassword { get; set; }

        [MaxLength(50)]
        [Column("strSMTPHost", TypeName = "nvarchar(50)")]
        public string? StrSMTPHost { get; set; }

        [Column("intSMTPPort")]
        public int? IntSMTPPort { get; set; }

        [Column("boolEnabledSSL")]
        public bool? BoolEnabledSSL { get; set; }
    }
}
