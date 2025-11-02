using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class votingNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterAddresses_VotingDistricts_VotingDistrictId",
                table: "VoterAddresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "VotingDistrictId",
                table: "VoterAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAddresses_VotingDistricts_VotingDistrictId",
                table: "VoterAddresses",
                column: "VotingDistrictId",
                principalTable: "VotingDistricts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterAddresses_VotingDistricts_VotingDistrictId",
                table: "VoterAddresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "VotingDistrictId",
                table: "VoterAddresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAddresses_VotingDistricts_VotingDistrictId",
                table: "VoterAddresses",
                column: "VotingDistrictId",
                principalTable: "VotingDistricts",
                principalColumn: "Id");
        }
    }
}
