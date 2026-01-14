using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class CharacteristicVm
    {
        public int Id { get; set; }

        public string? CharacteristicName { get; set; }

        public int? MeaTypeId { get; set; }

        public int? ProcessId { get; set; }

        public int? CharacteristicType { get; set; }

        public string? CharacteristicUnit { get; set; }

        public bool? Deleted { get; set; }

        public int? DefectRateLimit { get; set; }

        public int? EventEnable { get; set; }

        public int? EmailEventModel { get; set; }

        public int? Decimals { get; set; }
    }
}
