using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventRoles")]
    public class EventRoles
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [Column("intCharacteristicID")]
        public int? IntCharacteristicID { get; set; }

        [MaxLength(50)]
        [Column("strRoleID", TypeName = "nvarchar(50)")]
        public string? StrRoleID { get; set; }
    }
}
