using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RStore.Api.Migrations
{
    public partial class SeededDefaultUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD", "06c4165b-8773-42f4-9397-165e9aeff9e8", "Admin", "ADMIN" },
                    { "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB", "aa3905e0-113b-4b6e-970d-19eb0a9232e5", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "07928237-EBFE-41EE-9017-8C3B691DCA08", 0, "a3efceb6-111d-42f0-84ff-3268006ff540", "admin@rstore.com", false, "Admin", "System", false, null, "ADMIN@RSTORE.COM", "ADMIN@RSTORE.COM", "AQAAAAEAACcQAAAAEKObxVqkXnY9TO+JEbi4BTtlB/mWe8q1OfVt/Gcbf7jlbZMZhfB5glnXVd2ihOYrbg==", null, false, "a290ceba-2a41-4d0a-a5a3-15b5f53f510c", false, "admin@rstore.com" },
                    { "197A6B96-01D9-4CA8-AF39-77B65DE60F90", 0, "e73a699e-f27e-4b2c-9b41-ab43fdf43608", "user@rstore.com", false, "User", "System", false, null, "USER@RSTORE.COM", "USER@RSTORE.COM", "AQAAAAEAACcQAAAAEB8lkBuzBncCWpRC6M5QMTIQ8vZ17o5riCq+XC1Vq/SBnVEu3KbGDDfQQtO/iBQM7Q==", null, false, "f2d3ddf6-b084-4bec-9a4e-bb79fce8decb", false, "user@rstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD", "07928237-EBFE-41EE-9017-8C3B691DCA08" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB", "197A6B96-01D9-4CA8-AF39-77B65DE60F90" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD", "07928237-EBFE-41EE-9017-8C3B691DCA08" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB", "197A6B96-01D9-4CA8-AF39-77B65DE60F90" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07928237-EBFE-41EE-9017-8C3B691DCA08");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "197A6B96-01D9-4CA8-AF39-77B65DE60F90");
        }
    }
}
