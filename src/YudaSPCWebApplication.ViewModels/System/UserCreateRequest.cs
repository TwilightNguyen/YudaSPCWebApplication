using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class UserCreateRequest
    {

        public string? RoleID { get; set; }

        public required string EmailAddress { get; set; }

        public required string FullName { get; set; }

        public required string Department { get; set; }

        public required string StaffID { get; set; }

        public int? Enable { get; set; }

        public string? SelectedAreaID { get; set; }

        public required string Password { get; set; }
    }
}
