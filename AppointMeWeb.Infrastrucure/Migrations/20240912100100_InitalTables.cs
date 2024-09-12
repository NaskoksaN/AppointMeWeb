﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointMeWeb.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class InitalTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Application user data");

            migrationBuilder.AddColumn<int>(
                name: "BusinessServiceProviderId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                comment: "Associated business service provider");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "User date of birth");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Application first name");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Application last name");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Application phone");

            migrationBuilder.CreateTable(
                name: "BusinessServiceProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Buinsess identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Buinsess name"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Buinsess description"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Buinsess E-mail"),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Buinsess phone"),
                    Town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Buinsess town"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Buinsess address"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Buinsess web-link")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessServiceProviders", x => x.Id);
                },
                comment: "Details of service providers");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Appoinment identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Message to BusinessServiceProvider"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Appoinment confirmation"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User Identifier"),
                    BusinessServiceProviderId = table.Column<int>(type: "int", nullable: false, comment: "BusinessServiceProvider Identifier"),
                    Day = table.Column<int>(type: "int", nullable: false, comment: "Day of the week"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Start of appointment"),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false, comment: "End of appointment"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_BusinessServiceProviders_BusinessServiceProviderId",
                        column: x => x.BusinessServiceProviderId,
                        principalTable: "BusinessServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Appoinment");

            migrationBuilder.CreateTable(
                name: "WorkingSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Schedule identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessServiceProviderId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to BusinessServiceProvider"),
                    Day = table.Column<int>(type: "int", nullable: false, comment: "Day of the week"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Start time of work"),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false, comment: "End time of work"),
                    AppointmentDuration = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Duration of each appointment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingSchedules_BusinessServiceProviders_BusinessServiceProviderId",
                        column: x => x.BusinessServiceProviderId,
                        principalTable: "BusinessServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Working schedule for each service provider");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BusinessServiceProviderId",
                table: "AspNetUsers",
                column: "BusinessServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ApplicationUserId",
                table: "Appointments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BusinessServiceProviderId",
                table: "Appointments",
                column: "BusinessServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSchedules_BusinessServiceProviderId",
                table: "WorkingSchedules",
                column: "BusinessServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BusinessServiceProviders_BusinessServiceProviderId",
                table: "AspNetUsers",
                column: "BusinessServiceProviderId",
                principalTable: "BusinessServiceProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BusinessServiceProviders_BusinessServiceProviderId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "WorkingSchedules");

            migrationBuilder.DropTable(
                name: "BusinessServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BusinessServiceProviderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessServiceProviderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Application user data");
        }
    }
}
