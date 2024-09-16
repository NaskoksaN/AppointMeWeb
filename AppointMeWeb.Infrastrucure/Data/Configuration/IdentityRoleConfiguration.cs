using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Infrastrucure.Data.Configuration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = "adminRoleId", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "businessRoleId", Name = "Business", NormalizedName = "BUSINESS" },
                new IdentityRole { Id = "webUserRoleId", Name = "WebUser", NormalizedName = "WEBUSER" }
            );
        }
    }
}
