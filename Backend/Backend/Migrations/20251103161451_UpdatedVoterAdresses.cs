using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVoterAdresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "VoterAddresses",
                newName: "CityName");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumberEnd",
                table: "VoterAddresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseNumberStart",
                table: "VoterAddresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumberEnd",
                table: "VoterAddresses");

            migrationBuilder.DropColumn(
                name: "HouseNumberStart",
                table: "VoterAddresses");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "VoterAddresses",
                newName: "HouseNumber");
        }
    }
}
