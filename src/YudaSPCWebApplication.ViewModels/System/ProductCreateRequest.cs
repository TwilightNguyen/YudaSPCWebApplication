using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class ProductCreateRequest
    {

        public required string Name { get; set; }

        public required int AreaId { get; set; }

        public required int InspPlanId { get; set; }

        public string? ModelInternal { get; set; }

        public string? CustomerName { get; set; }

        public string? Notes { get; set; }

        public string? Description { get; set; }

        public int? MoldQty { get; set; }

        public int? CavityQty { get; set; }
    }
}
