using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System.InspectionPlanTracking
{
    public class InspectionPlanTrackingVm
    { 
        public int IntID { get; set; }

        public int? InspPlanID { get; set; }

        public int? PlanTypeID { get; set; }

        public int? PlanState { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UserID { get; set; }

        // Additional property not in the entity
        public string? UserName { get; set; }
        public string? InspPlanName { get; set; }
        public string? PlanTypeName { get; set; }
    }
}
