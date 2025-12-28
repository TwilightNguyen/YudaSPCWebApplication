namespace YudaSPCWebApplication.ViewModels.System
{
    public class RoleVm
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int IntRoleUser { get; set; }
        public int IntLevel { get; set; }
        public int IntRoleID { get; set; }
    }
}
