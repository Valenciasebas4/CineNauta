using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine_Nauta.Migrations
{
    public partial class NewTableTemporalSaleandReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Movies_MovieId",
                table: "Functions");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Functions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FunctionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemporalSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FunctionId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporalSales_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TemporalSales_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ReservationId",
                table: "Seats",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FunctionId",
                table: "Reservations",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSales_FunctionId",
                table: "TemporalSales",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalSales_UserId",
                table: "TemporalSales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Functions_Movies_MovieId",
                table: "Functions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Reservations_ReservationId",
                table: "Seats",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Movies_MovieId",
                table: "Functions");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Reservations_ReservationId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "TemporalSales");

            migrationBuilder.DropIndex(
                name: "IX_Seats_ReservationId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Functions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Functions_Movies_MovieId",
                table: "Functions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
