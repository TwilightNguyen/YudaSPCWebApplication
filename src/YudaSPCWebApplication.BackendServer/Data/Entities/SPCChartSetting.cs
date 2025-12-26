using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbSPCChartSetting")]
    public class SPCChartSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intChartTypeID")]
        public int IntChartQty { get; set; }

        [Column("intPointQty")]
        public int IntPointQty { get; set; }

        [Column("intFontSize")]
        public int IntFontSize { get; set; }

        [Column("intYAxisTick")]
        public int IntYAxisTick { get; set; }

        [Column("intXAxisTick")]
        public int IntXAxisTick { get; set; }
    }
}
