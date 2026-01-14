using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class CharacteristicCreateRequest
    {
        public required string CharacteristicName { get; set; }

        public required int MeaTypeId { get; set; }

        public required int ProcessId { get; set; }

        public required int CharacteristicType { get; set; }

        public string? CharacteristicUnit { get; set; }

        public int? DefectRateLimit { get; set; }

        public int? EventEnable { get; set; }

        public required int EmailEventModel { get; set; }

        public required int Decimals { get; set; }
    }
}
