using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System.Production
{
    public class ProductionCreateRequest
    {
        public required int LineId { get; set; }

        public required int JobId { get; set; }

        public string? Notes { get; set; }

        public DateTime? ProductionDate { get; set; }

        public int? ProductionQty { get; set; }

        public string? LotInform { get; set; }

        public string? MaterialInform { get; set; }
    }
}
