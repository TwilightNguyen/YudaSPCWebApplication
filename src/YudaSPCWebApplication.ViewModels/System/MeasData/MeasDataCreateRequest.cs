using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System.MeasData
{
    public class MeasDataCreateRequest
    {
        public required int ProductionId { get; set; }

        public required int PlanTypeId { get; set; }

        public required int MoldId { get; set; }

        public required int CavityId { get; set; }

        public required List<MeasDataValue> Values { get; set; }

        public string? CharacteristicRange { get; set; }

        public string? OutputNotes { get; set; }

        public int? SampleQty { get; set; }
    }
}
