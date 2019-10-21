using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyMVC.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Speaker = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Conferences",
                columns: new[] { "Id", "Location", "Name", "Start" },
                values: new object[] { 1, "28 Nguyen Tri Phuong", "Bob", new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Conferences",
                columns: new[] { "Id", "Location", "Name", "Start" },
                values: new object[] { 2, "28 Nguyen Tri Phuong", "Andy", new DateTime(2019, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Conferences",
                columns: new[] { "Id", "Location", "Name", "Start" },
                values: new object[] { 3, "28 Nguyen Tri Phuong", "Jame", new DateTime(2019, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "ConferenceId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Mai Ba Quan" },
                    { 2, 1, "Vinh Hoang" },
                    { 3, 2, "Thanh Bon" },
                    { 4, 2, "Nhi Uyen" }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "Approved", "ConferenceId", "Speaker", "Title" },
                values: new object[,]
                {
                    { 1, false, 1, "Le Tuong Phuc", "Understanding ASP.NET Core MVC" },
                    { 2, false, 2, "Anh Khoa", "Understanding ASP.NET Core Dapper" },
                    { 3, false, 2, "Si Lun", "Understanding Write CV" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_ConferenceId",
                table: "Attendees",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ConferenceId",
                table: "Proposals",
                column: "ConferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Conferences");
        }
    }
}
