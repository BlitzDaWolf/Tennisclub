using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisClub.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGenders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLeagues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLeagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    FederationNr = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(35)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(70)", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    Addition = table.Column<string>(type: "VARCHAR(2)", nullable: true),
                    Zipcode = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    PhoneNr = table.Column<string>(type: "VARCHAR(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMembers_tblGenders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tblGenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblGames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNumber = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGames_tblLeagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "tblLeagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblGames_tblMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "tblMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblMemberFines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    FineNumber = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(7, 2)", nullable: false),
                    handOutDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMemberFines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMemberFines_tblMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "tblMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblMemberRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    RoleId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMemberRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMemberRoles_tblMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "tblMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblMemberRoles_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblGameResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    SetNr = table.Column<int>(type: "INT", maxLength: 3, nullable: false),
                    ScoreTeamMember = table.Column<int>(type: "INT", maxLength: 3, nullable: false),
                    ScoreOpponent = table.Column<int>(type: "INT", maxLength: 3, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGameResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGameResults_tblGames_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblGenders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "tblLeagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Junoir" },
                    { 2, "Pro" }
                });

            migrationBuilder.InsertData(
                table: "tblRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblGameResults_GameId",
                table: "tblGameResults",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGameResults_SetNr_GameId",
                table: "tblGameResults",
                columns: new[] { "SetNr", "GameId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblGames_GameNumber",
                table: "tblGames",
                column: "GameNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblGames_LeagueId",
                table: "tblGames",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGames_MemberId",
                table: "tblGames",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGenders_Name",
                table: "tblGenders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblLeagues_Name",
                table: "tblLeagues",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblMemberFines_FineNumber",
                table: "tblMemberFines",
                column: "FineNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblMemberFines_MemberId",
                table: "tblMemberFines",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMemberRoles_RoleId",
                table: "tblMemberRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMemberRoles_MemberId_RoleId_StartDate_EndDate",
                table: "tblMemberRoles",
                columns: new[] { "MemberId", "RoleId", "StartDate", "EndDate" },
                unique: true,
                filter: "[EndDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblMembers_FederationNr",
                table: "tblMembers",
                column: "FederationNr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblMembers_GenderId",
                table: "tblMembers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRoles_Name",
                table: "tblRoles",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblGameResults");

            migrationBuilder.DropTable(
                name: "tblMemberFines");

            migrationBuilder.DropTable(
                name: "tblMemberRoles");

            migrationBuilder.DropTable(
                name: "tblGames");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblLeagues");

            migrationBuilder.DropTable(
                name: "tblMembers");

            migrationBuilder.DropTable(
                name: "tblGenders");
        }
    }
}
