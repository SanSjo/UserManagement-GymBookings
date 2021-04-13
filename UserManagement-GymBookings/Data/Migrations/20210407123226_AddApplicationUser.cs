using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement_GymBookings.Data.Migrations
{
    public partial class AddApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_AspNetUsers_ApplicationUserID",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_GymClass_GymClassID",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymClass",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ApplicationUserGymClass",
                newName: "AttendingMember");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserGymClass_GymClassID",
                table: "AttendingMember",
                newName: "IX_AttendingMember_GymClassID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendingMember",
                table: "AttendingMember",
                columns: new[] { "ApplicationUserID", "GymClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttendingMember_AspNetUsers_ApplicationUserID",
                table: "AttendingMember",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendingMember_GymClass_GymClassID",
                table: "AttendingMember",
                column: "GymClassID",
                principalTable: "GymClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendingMember_AspNetUsers_ApplicationUserID",
                table: "AttendingMember");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendingMember_GymClass_GymClassID",
                table: "AttendingMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendingMember",
                table: "AttendingMember");

            migrationBuilder.RenameTable(
                name: "AttendingMember",
                newName: "ApplicationUserGymClass");

            migrationBuilder.RenameIndex(
                name: "IX_AttendingMember_GymClassID",
                table: "ApplicationUserGymClass",
                newName: "IX_ApplicationUserGymClass_GymClassID");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymClass",
                table: "ApplicationUserGymClass",
                columns: new[] { "ApplicationUserID", "GymClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_AspNetUsers_ApplicationUserID",
                table: "ApplicationUserGymClass",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_GymClass_GymClassID",
                table: "ApplicationUserGymClass",
                column: "GymClassID",
                principalTable: "GymClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
