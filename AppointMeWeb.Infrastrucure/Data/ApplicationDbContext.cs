
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Infrastrucure.Data.Configuration;

namespace AppointMeWeb.Infrastrucure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        private readonly bool seedDb;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool _seedDb=true)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
            else
            {
                Database.EnsureCreated();
            }

            seedDb = _seedDb;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            if (seedDb)
            {
                builder.ApplyConfiguration(new IdentityRoleConfiguration());
            }
            

            base.OnModelCreating(builder);
        }
        public DbSet<BusinessServiceProvider> BusinessServiceProviders { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
