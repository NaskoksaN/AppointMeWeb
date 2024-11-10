using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointMeWeb.Infrastrucure.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(u => u.ApplicationUser)
                   .WithMany(r => r.Ratings)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u=> u.BusinessServiceProvider)
                   .WithMany(b=> b.Ratings)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
