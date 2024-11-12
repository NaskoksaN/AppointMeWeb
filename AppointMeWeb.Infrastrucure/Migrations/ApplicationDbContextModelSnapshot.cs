﻿// <auto-generated />
using System;
using AppointMeWeb.Infrastrucure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointMeWeb.Infrastrucure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("BusinessServiceProviderId")
                        .HasColumnType("int")
                        .HasComment("Associated business service provider");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasComment("User date of birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Application first name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("User activity");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Application last name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Application phone");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessServiceProviderId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.HasComment("Application user data");
                        });
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Appoinment identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessServiceProviderId")
                        .HasColumnType("int")
                        .HasComment("BusinessServiceProvider Identifier");

                    b.Property<DateOnly>("Day")
                        .HasColumnType("date")
                        .HasComment("Day of the week");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time")
                        .HasComment("End of appointment");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit")
                        .HasComment("Slot availability status");

                    b.Property<string>("Message")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Message to BusinessServiceProvider");

                    b.Property<int?>("RatingId")
                        .HasColumnType("int")
                        .HasComment("User comment");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time")
                        .HasComment("Start of appointment");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Appoinment confirmation");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.HasKey("Id");

                    b.HasIndex("BusinessServiceProviderId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments", t =>
                        {
                            t.HasComment("Appoinment");
                        });
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Buinsess identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Buinsess address");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("AppointmentDuration")
                        .HasColumnType("time")
                        .HasComment("Duration of each appointment");

                    b.Property<string>("BusinessDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Buinsess description");

                    b.Property<int>("BusinessType")
                        .HasColumnType("int")
                        .HasComment("Type of buinsess section");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("BusinessProvier activity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Buinsess name");

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Buinsess town");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasComment("Buinsess web-link");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("BusinessServiceProviders", t =>
                        {
                            t.HasComment("Details of service providers");
                        });
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Rating Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppointmentComment")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("User comment");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("Evaluation")
                        .HasColumnType("int")
                        .HasComment("User evaluation for service");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("Ratings", t =>
                        {
                            t.HasComment("User business rating");
                        });
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.WorkingSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Schedule identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessServiceProviderId")
                        .HasColumnType("int")
                        .HasComment("Foreign key to BusinessServiceProvider");

                    b.Property<int>("Day")
                        .HasColumnType("int")
                        .HasComment("Day of the week");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time")
                        .HasComment("End time of work");

                    b.Property<bool>("IsDayOff")
                        .HasColumnType("bit")
                        .HasComment("Indicates whether the day is a day off for the service provider.");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time")
                        .HasComment("Start time of work");

                    b.HasKey("Id");

                    b.HasIndex("BusinessServiceProviderId");

                    b.ToTable("WorkingSchedules", t =>
                        {
                            t.HasComment("Working schedule for each service provider");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator().HasValue("IdentityRole<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<string>");

                    b.HasDiscriminator().HasValue("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "adminRoleId",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "businessRoleId",
                            Name = "Business",
                            NormalizedName = "BUSINESS"
                        },
                        new
                        {
                            Id = "webUserRoleId",
                            Name = "WebUser",
                            NormalizedName = "WEBUSER"
                        });
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", "BusinessServiceProvider")
                        .WithMany()
                        .HasForeignKey("BusinessServiceProviderId");

                    b.Navigation("BusinessServiceProvider");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.Appointment", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", "BusinessServiceProvider")
                        .WithMany("Appointments")
                        .HasForeignKey("BusinessServiceProviderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("BusinessServiceProvider");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.Rating", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Ratings")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.Appointment", "Appointment")
                        .WithOne("Rating")
                        .HasForeignKey("AppointMeWeb.Infrastrucure.Data.Models.Rating", "AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.WorkingSchedule", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", "BusinessServiceProvider")
                        .WithMany("WorkingSchedules")
                        .HasForeignKey("BusinessServiceProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessServiceProvider");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.Appointment", b =>
                {
                    b.Navigation("Rating");
                });

            modelBuilder.Entity("AppointMeWeb.Infrastrucure.Data.Models.BusinessServiceProvider", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("WorkingSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
