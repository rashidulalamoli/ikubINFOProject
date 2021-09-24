using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ikubINFO.Api.Migrations
{
    public partial class AdditionalRowInserted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleGuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedDate", "RoleGuid", "Title" },
                values: new object[] { 1, new DateTime(2021, 9, 23, 4, 42, 24, 571, DateTimeKind.Utc).AddTicks(8521), "", true, false, new DateTime(2021, 9, 23, 4, 42, 24, 571, DateTimeKind.Utc).AddTicks(9364), "7b68d63b-2da4-4622-abd3-5fc9a94e64fe", "User" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedDate", "RoleGuid", "Title" },
                values: new object[] { 2, new DateTime(2021, 9, 23, 4, 42, 24, 572, DateTimeKind.Utc).AddTicks(117), "", true, false, new DateTime(2021, 9, 23, 4, 42, 24, 572, DateTimeKind.Utc).AddTicks(123), "00f3a44b-570f-4bb3-bbe6-45a4aeca977d", "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedDate", "RoleGuid", "Title" },
                values: new object[] { 3, new DateTime(2021, 9, 23, 4, 42, 24, 572, DateTimeKind.Utc).AddTicks(681), "", true, false, new DateTime(2021, 9, 23, 4, 42, 24, 572, DateTimeKind.Utc).AddTicks(692), "36ca7f43-41ae-4af3-a30b-a943dbde148b", "Super Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
