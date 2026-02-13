using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System.Production
{
    public class ProductionVm
    {
        public int Id { get; set; }

        public int? LineId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? JobId { get; set; }

        public int? UserId { get; set; }

        public string? Notes { get; set; }
        
        public DateTime? ProductionDate { get; set; }

        public int? ProductionQty { get; set; }

        public string? LotInform { get; set; }

        public string? MaterialInform { get; set; }

    }
}
