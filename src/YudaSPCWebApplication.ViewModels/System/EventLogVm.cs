using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class EventLogVm
    {
        public required int Id { get; set; }

        public DateTime? EventTime { get; set; }

        public string? EventCode { get; set; }


        public string? Event { get; set; }

        public string? Station { get; set; }
    }
}
