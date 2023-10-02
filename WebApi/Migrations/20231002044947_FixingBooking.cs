using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class FixingBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_room_guid",
                table: "tb_tr_bookings");

            migrationBuilder.AddColumn<Guid>(
                name: "employee_guid",
                table: "tb_tr_bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropColumn(
                name: "employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_room_guid",
                table: "tb_tr_bookings",
                column: "room_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
