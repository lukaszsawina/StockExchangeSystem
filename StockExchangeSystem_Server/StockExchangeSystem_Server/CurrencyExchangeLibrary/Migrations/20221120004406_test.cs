using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeLibrary.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRefreshed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Crypto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetaDataID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crypto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Crypto_CryptoData_MetaDataID",
                        column: x => x.MetaDataID,
                        principalTable: "CryptoData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OHLCData",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpenUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HighUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CloseUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CryptoModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHLCData", x => new { x.Symbol, x.Time });
                    table.ForeignKey(
                        name: "FK_OHLCData_Crypto_CryptoModelID",
                        column: x => x.CryptoModelID,
                        principalTable: "Crypto",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crypto_MetaDataID",
                table: "Crypto",
                column: "MetaDataID");

            migrationBuilder.CreateIndex(
                name: "IX_OHLCData_CryptoModelID",
                table: "OHLCData",
                column: "CryptoModelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OHLCData");

            migrationBuilder.DropTable(
                name: "Crypto");

            migrationBuilder.DropTable(
                name: "CryptoData");
        }
    }
}
