using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointMeWeb.Infrastrucure.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasOne(u => u.ApplicationUser)
                .WithMany(a => a.Appointments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.BusinessServiceProvider)
               .WithMany(a => a.Appointments)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
