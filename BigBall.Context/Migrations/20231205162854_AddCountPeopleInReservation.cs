using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBall.Context.Migrations
{
    public partial class AddCountPeopleInReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountPeople",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountPeople",
                table: "Reservations");
        }
    }
}
