using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class JobDecisionVm
    {
        public int Id { get; set; }

        public string? Decision { get; set; }

        public int? ColorCode { get; set; }
    }
}
