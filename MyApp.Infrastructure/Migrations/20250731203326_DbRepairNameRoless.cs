using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbRepairNameRoless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolPermiso_Roless_RolId",
                table: "RolPermiso");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roless_RolId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roless",
                table: "Roless");

            migrationBuilder.RenameTable(
                name: "Roless",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermiso_Roles_RolId",
                table: "RolPermiso",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolPermiso_Roles_RolId",
                table: "RolPermiso");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roless");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roless",
                table: "Roless",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermiso_Roless_RolId",
                table: "RolPermiso",
                column: "RolId",
                principalTable: "Roless",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roless_RolId",
                table: "Users",
                column: "RolId",
                principalTable: "Roless",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
