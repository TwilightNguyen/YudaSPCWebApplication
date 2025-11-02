using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbSPCChartSetting")]
    public class SPCChartSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int IntChartQty { get; set; }

        public int IntPointQty { get; set; }

        public int IntFontSize { get; set; }

        public int IntYAxisTick { get; set; }

        public int IntXAxisTick { get; set; }
    }
}
