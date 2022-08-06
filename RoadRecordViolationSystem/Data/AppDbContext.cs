using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.EntityConfigurations;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UsersInformation> UsersInformations { get; set; }
        public DbSet<UsersLogHistory> UsersLogHistories { get; set; }
        public DbSet<ViolationTypes> ViolationTypes { get; set; }
        public DbSet<Violations> Violations { get; set; }
        public DbSet<LicenseDetails> LicenseDetails { get; set; }
        public DbSet<VehicleInformation> vehicleInformations { get; set; }
        public DbSet<Violator> Violators { get; set; }
        public DbSet<AccidentInvolve> AccidentInvolves { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<ViolatorTicketInformation> ViolatorTicketInformations { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<ContestSchedule> ContestSchedules { get; set; }
        public DbSet<OfficerInvolve> OfficerInvolves { get; set; }


        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {
        }

        public AppDBContext()
            :base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UsersInformationEntityConfiguration());
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            builder.ApplyConfiguration(new UsersLogHistoryEntityConfiguration());
            builder.ApplyConfiguration(new ViolationTypesEntityConfiguration());
            builder.ApplyConfiguration(new ViolationsEntityConfigurations());
            builder.ApplyConfiguration(new LicenseDetailsEntityConfigurations());
            builder.ApplyConfiguration(new VehicleInformationEntityConfiguration());
            builder.ApplyConfiguration(new ViolatorEntityConfiguration());
            builder.ApplyConfiguration(new AccidentInvolveEntityConfiguration());
            builder.ApplyConfiguration(new ViolatorTicketInformationEntityConfiguration());
            builder.ApplyConfiguration(new AccidentEntityConfiguration());
            builder.ApplyConfiguration(new SettingEntityConfiguration());
            builder.ApplyConfiguration(new ContestEntityConfiguration());
            builder.ApplyConfiguration(new ContestScheduleEntityConfiguration());
            builder.ApplyConfiguration(new OfficerInvolveEntityConfiguration());
        }
    }
}
