using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbRole")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntRoleID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrRoleName { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? StrDescription { get; set; }

        public int IntLevel { get; set; }

        public int? IntRoleUser { get; set; }
    }
}
