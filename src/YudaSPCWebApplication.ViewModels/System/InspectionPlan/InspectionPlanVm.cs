using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System.InspectionPlan
{
    public class InspectionPlanVm
    { 
        public required int Id { get; set; }

        public string? Name { get; set; }

        public int? AreaId { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
