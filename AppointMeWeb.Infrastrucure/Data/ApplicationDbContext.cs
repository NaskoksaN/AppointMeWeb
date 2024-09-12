
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Infrastrucure.Data.Models;

namespace AppointMeWeb.Infrastrucure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessServiceProvider> BusinessServiceProviders { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
