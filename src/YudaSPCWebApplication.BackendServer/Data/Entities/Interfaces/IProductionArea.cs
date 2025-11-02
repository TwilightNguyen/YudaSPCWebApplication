using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities.Interfaces
{
    public class IProductionArea
    {
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        string? StrNameArea { get; set; }
    }
}
