using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data.Entities;

namespace YudaSPCWebApplication.BackendServer.Data
{
    public class ApplicationDbContext : DbContext //IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
        )
            : base(options)
        {
        }

        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<TVDisplay> TVDisplays { get; set; }
        public virtual DbSet<MeasData3_01> MeasDatas { get; set; }
        public virtual DbSet<MeasureType> MeasureTypes { get; set; }
        public virtual DbSet<ProductionArea> ProductionAreas { get; set; }
        public virtual DbSet<InspectionPlanType> InspPlanTypes { get; set; }
        public virtual DbSet<ProductionData> ProductionDatas { get; set; }
        public virtual DbSet<ProcessLine> ProcessLines { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Characteristic> Characteristices { get; set; }
        public virtual DbSet<ProductName> ProductNames { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<JobDecision> JobDecisions { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<JobData> JobDatas { get; set; }
        public virtual DbSet<EventRoles> AlarmEvents { get; set; }
        public virtual DbSet<EmailServer> EmailServers { get; set; }
        public virtual DbSet<WebSession> WebSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
            .Property(r => r.IntRoleID)
            .ValueGeneratedNever(); // Prevent identity generation on this property


            modelBuilder.Entity<User>()
            .Property(r => r.IntUserID)
            .ValueGeneratedNever(); // Prevent identity generation on this property

            modelBuilder.HasSequence("KnowledgeBaseSepence");
        }
    }
}
