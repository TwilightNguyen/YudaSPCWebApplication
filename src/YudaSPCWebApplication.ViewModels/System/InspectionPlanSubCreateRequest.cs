using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class InspectionPlanSubCreateRequest
    {
        public required int InspPlanId { get; set; }
        public required int PlanTypeId { get; set; }
    }
}
