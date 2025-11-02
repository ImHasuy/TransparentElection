using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Intialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationalMinorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartyName = table.Column<string>(type: "text", nullable: false),
                    LogoPath = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    NationalMinoritiesType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalMinorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LogoPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VotingDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountyCode = table.Column<string>(type: "text", nullable: false),
                    CountyName = table.Column<string>(type: "text", nullable: false),
                    OEVK = table.Column<string>(type: "text", nullable: false),
                    CityCode = table.Column<string>(type: "text", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    TEVK = table.Column<string>(type: "text", nullable: false),
                    PollingStationNumber = table.Column<string>(type: "text", nullable: false),
                    PollingStationAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredNationalMinorityCandidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RankInList = table.Column<int>(type: "integer", nullable: false),
                    NationalMinoritiesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredNationalMinorityCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredNationalMinorityCandidates_NationalMinorities_Nat~",
                        column: x => x.NationalMinoritiesId,
                        principalTable: "NationalMinorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredPartyListCandidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RankInList = table.Column<int>(type: "integer", nullable: false),
                    PartyListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredPartyListCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredPartyListCandidates_PartyLists_PartyListId",
                        column: x => x.PartyListId,
                        principalTable: "PartyLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EligibleVoters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IDCardNumber = table.Column<string>(type: "text", nullable: false),
                    ResidenceCardNumber = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<int>(type: "integer", nullable: false),
                    IsNationalMinorityVoter = table.Column<bool>(type: "boolean", nullable: false),
                    NationalMinoritiesEnum = table.Column<int>(type: "integer", nullable: false),
                    VotingDistinctId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibleVoters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EligibleVoters_VotingDistricts_VotingDistinctId",
                        column: x => x.VotingDistinctId,
                        principalTable: "VotingDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleMemberCandidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PartyName = table.Column<string>(type: "text", nullable: false),
                    VotingDistinctId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleMemberCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleMemberCandidates_VotingDistricts_VotingDistinctId",
                        column: x => x.VotingDistinctId,
                        principalTable: "VotingDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoterAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false),
                    StreetName = table.Column<string>(type: "text", nullable: false),
                    StreetType = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Staircase = table.Column<string>(type: "text", nullable: true),
                    VotingDistrictId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoterAddresses_VotingDistricts_VotingDistrictId",
                        column: x => x.VotingDistrictId,
                        principalTable: "VotingDistricts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EligibleVoters_IDCardNumber",
                table: "EligibleVoters",
                column: "IDCardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EligibleVoters_ResidenceCardNumber",
                table: "EligibleVoters",
                column: "ResidenceCardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EligibleVoters_VotingDistinctId",
                table: "EligibleVoters",
                column: "VotingDistinctId");

            migrationBuilder.CreateIndex(
                name: "IX_EligibleVoters_ZipCode",
                table: "EligibleVoters",
                column: "ZipCode");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredNationalMinorityCandidates_NationalMinoritiesId",
                table: "RegisteredNationalMinorityCandidates",
                column: "NationalMinoritiesId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredPartyListCandidates_PartyListId",
                table: "RegisteredPartyListCandidates",
                column: "PartyListId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMemberCandidates_VotingDistinctId",
                table: "SingleMemberCandidates",
                column: "VotingDistinctId");

            migrationBuilder.CreateIndex(
                name: "IX_VoterAddresses_VotingDistrictId",
                table: "VoterAddresses",
                column: "VotingDistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "EligibleVoters");

            migrationBuilder.DropTable(
                name: "RegisteredNationalMinorityCandidates");

            migrationBuilder.DropTable(
                name: "RegisteredPartyListCandidates");

            migrationBuilder.DropTable(
                name: "SingleMemberCandidates");

            migrationBuilder.DropTable(
                name: "VoterAddresses");

            migrationBuilder.DropTable(
                name: "NationalMinorities");

            migrationBuilder.DropTable(
                name: "PartyLists");

            migrationBuilder.DropTable(
                name: "VotingDistricts");
        }
    }
}
