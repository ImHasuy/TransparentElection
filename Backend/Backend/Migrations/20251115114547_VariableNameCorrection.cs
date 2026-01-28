using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class VariableNameCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "validUntil",
                table: "VotingTokens",
                newName: "ValidUntil");

            migrationBuilder.RenameColumn(
                name: "isUsed",
                table: "VotingTokens",
                newName: "IsUsed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidUntil",
                table: "VotingTokens",
                newName: "validUntil");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "VotingTokens",
                newName: "isUsed");
        }
    }
}
