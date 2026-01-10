using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class UserVm
    {
        public int UserID { get; set; }

        public string? RoleID { get; set; }

        public required string EmailAddress { get; set; }

        public required string FullName { get; set; }

        public required string Department { get; set; }

        public required string StaffID { get; set; }

        public DateTime? LastActivityTime { get; set; }

        public int? Enable { get; set; }

        public string? SelectedAreaID { get; set; }
    }
}
