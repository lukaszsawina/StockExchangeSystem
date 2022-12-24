using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeLibrary.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

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
                name: "CurrencyData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fromSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRefreshed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StockData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRefreshed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockData", x => x.ID);
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
                name: "Currency",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetaDataID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyData_MetaDataID",
                        column: x => x.MetaDataID,
                        principalTable: "CurrencyData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetaDataID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stock_StockData_MetaDataID",
                        column: x => x.MetaDataID,
                        principalTable: "StockData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OHLCCryptoData",
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
                    table.PrimaryKey("PK_OHLCCryptoData", x => new { x.Symbol, x.Time });
                    table.ForeignKey(
                        name: "FK_OHLCCryptoData_Crypto_CryptoModelID",
                        column: x => x.CryptoModelID,
                        principalTable: "Crypto",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OHLCCurrenciesData",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CloseUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHLCCurrenciesData", x => new { x.Symbol, x.Time });
                    table.ForeignKey(
                        name: "FK_OHLCCurrenciesData_Currency_CurrencyModelID",
                        column: x => x.CurrencyModelID,
                        principalTable: "Currency",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OHLCVStockData",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHLCVStockData", x => new { x.Symbol, x.Time });
                    table.ForeignKey(
                        name: "FK_OHLCVStockData_Stock_StockModelID",
                        column: x => x.StockModelID,
                        principalTable: "Stock",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crypto_MetaDataID",
                table: "Crypto",
                column: "MetaDataID");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_MetaDataID",
                table: "Currency",
                column: "MetaDataID");

            migrationBuilder.CreateIndex(
                name: "IX_OHLCCryptoData_CryptoModelID",
                table: "OHLCCryptoData",
                column: "CryptoModelID");

            migrationBuilder.CreateIndex(
                name: "IX_OHLCCurrenciesData_CurrencyModelID",
                table: "OHLCCurrenciesData",
                column: "CurrencyModelID");

            migrationBuilder.CreateIndex(
                name: "IX_OHLCVStockData_StockModelID",
                table: "OHLCVStockData",
                column: "StockModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_MetaDataID",
                table: "Stock",
                column: "MetaDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "OHLCCryptoData");

            migrationBuilder.DropTable(
                name: "OHLCCurrenciesData");

            migrationBuilder.DropTable(
                name: "OHLCVStockData");

            migrationBuilder.DropTable(
                name: "Crypto");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "CryptoData");

            migrationBuilder.DropTable(
                name: "CurrencyData");

            migrationBuilder.DropTable(
                name: "StockData");
        }
    }
}
