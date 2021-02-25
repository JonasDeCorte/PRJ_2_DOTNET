using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projecten2.Migrations
{
    public partial class ExtendIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActiefSinds",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRegistratie",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GebruikersNaam",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GegevensContactPersonen",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlantNummer",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naam",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersoneelsNummer",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Voornaam",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wachtwoord",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bedrijf",
                columns: table => new
                {
                    BedrijfsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefoonnummers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandHoofdzetel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Straat = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    KlantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijf", x => x.BedrijfsID);
                    table.ForeignKey(
                        name: "FK_Bedrijf_AspNetUsers_KlantId",
                        column: x => x.KlantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    ContractTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TijdstippenTicketAanmaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximaleAfhaaltijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinimaleAfhaaltijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prijs = table.Column<double>(type: "float", nullable: false),
                    ManierTicketAanmaken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.ContractTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doorlooptijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KlantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContractStatus = table.Column<int>(type: "int", nullable: false),
                    ContractTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractNr);
                    table.ForeignKey(
                        name: "FK_Contract_AspNetUsers_KlantId",
                        column: x => x.KlantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ContractType_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractType",
                        principalColumn: "ContractTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AanmaakDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opmerkingen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    ContractNr = table.Column<int>(type: "int", nullable: true),
                    OplossingBijlageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketNr);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Contract_ContractNr",
                        column: x => x.ContractNr,
                        principalTable: "Contract",
                        principalColumn: "ContractNr");
                });

            migrationBuilder.CreateTable(
                name: "Bijlage",
                columns: table => new
                {
                    BijlageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestandType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TicketNr = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bijlage", x => x.BijlageID);
                    table.ForeignKey(
                        name: "FK_Bijlage_Ticket_TicketNr",
                        column: x => x.TicketNr,
                        principalTable: "Ticket",
                        principalColumn: "TicketNr");
                });

            migrationBuilder.CreateTable(
                name: "Rapport",
                columns: table => new
                {
                    RapportNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RapportNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapport", x => x.RapportNr);
                    table.ForeignKey(
                        name: "FK_Rapport_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketNr");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bedrijf_KlantId",
                table: "Bedrijf",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bijlage_TicketNr",
                table: "Bijlage",
                column: "TicketNr");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_KlantId",
                table: "Contract",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapport_TicketId",
                table: "Rapport",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ContractNr",
                table: "Ticket",
                column: "ContractNr");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_GebruikerId",
                table: "Ticket",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OplossingBijlageID",
                table: "Ticket",
                column: "OplossingBijlageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Bijlage_OplossingBijlageID",
                table: "Ticket",
                column: "OplossingBijlageID",
                principalTable: "Bijlage",
                principalColumn: "BijlageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bijlage_Ticket_TicketNr",
                table: "Bijlage");

            migrationBuilder.DropTable(
                name: "Bedrijf");

            migrationBuilder.DropTable(
                name: "Rapport");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Bijlage");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropColumn(
                name: "ActiefSinds",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DatumRegistratie",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GebruikersNaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GegevensContactPersonen",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KlantNummer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Naam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersoneelsNummer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Voornaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Wachtwoord",
                table: "AspNetUsers");
        }
    }
}
