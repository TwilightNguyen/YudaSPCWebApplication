using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data.Entities;
//using EventLog = YudaSPCWebApplication.BackendServer.Data.Entities.EventLog;

namespace YudaSPCWebApplication.BackendServer.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<TVDisplay> TVDisplays { get; set; }
        public virtual DbSet<MeasData> MeasDatas { get; set; }
        public virtual DbSet<MeasureType> MeasureTypes { get; set; }
        public virtual DbSet<ProductionArea> ProductionAreas { get; set; }
        public virtual DbSet<InspectionPlanType> InspPlanTypes { get; set; }
        public virtual DbSet<ProductionData> ProductionDatas { get; set; }
        public virtual DbSet<ProcessLine> ProcessLines { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Characteristic> Characteristices { get; set; }
        public virtual DbSet<ProductName> ProductNames { get; set; }
        //public new virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<JobDecision> JobDecisions { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        //public new virtual DbSet<User> Users { get; set; }
        public virtual DbSet<JobData> JobDatas { get; set; }
        public virtual DbSet<EventRoles> AlarmEvents { get; set; }
        public virtual DbSet<EmailServer> EmailServers { get; set; }
        public virtual DbSet<WebSession> WebSessions { get; set; }
        //public virtual DbSet<EventEmailHistory> TbEventEmailHistorys { get; set; }
        //public virtual DbSet<View_ProductionData> View_ProductionData { get; set; }
        //public virtual DbSet<View_Database> View_Database { get; set; }
        //public virtual DbSet<View_EventEmailData> View_EventEmailDatas { get; set; }
        //public virtual DbSet<SP_OverviewDescription> Sp_OverviewDescription { get; set; }
        //public virtual DbSet<TbDetail> Sp_OverviewDescriptions { get; set; }
        //public virtual DbSet<View_InspectionPlanData> View_InspectionPlanDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence("KnowledgeBaseSepence");
        }
    }
}
