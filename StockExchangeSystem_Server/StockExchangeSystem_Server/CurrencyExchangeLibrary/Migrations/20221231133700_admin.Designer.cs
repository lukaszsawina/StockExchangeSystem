// <auto-generated />
using System;
using CurrencyExchangeLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CurrencyExchangeLibrary.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221231133700_admin")]
    partial class admin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Account.AccountModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Account");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Crypto.CryptoDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DCCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DCName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastRefreshed")
                        .HasColumnType("datetime2");

                    b.Property<string>("MarketCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CryptoData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MetaDataID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MetaDataID");

                    b.ToTable("Crypto");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Currency.CurrencyDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastRefreshed")
                        .HasColumnType("datetime2");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fromSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CurrencyData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Currency.CurrencyModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MetaDataID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MetaDataID");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCCryptoModel", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CloseUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("HighUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LowUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpenUSD")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Symbol", "Time");

                    b.ToTable("OHLCCryptoData");

                    b.HasDiscriminator<string>("Discriminator").HasValue("OHLCCryptoModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCCurrencyModel", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CloseUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CurrencyModelID")
                        .HasColumnType("int");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LowUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Symbol", "Time");

                    b.HasIndex("CurrencyModelID");

                    b.ToTable("OHLCCurrenciesData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVStockModel", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StockModelID")
                        .HasColumnType("int");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Symbol", "Time");

                    b.HasIndex("StockModelID");

                    b.ToTable("OHLCVStockData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Stock.StockDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastRefreshed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("StockData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Stock.StockModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MetaDataID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MetaDataID");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Account.UserModel", b =>
                {
                    b.HasBaseType("CurrencyExchangeLibrary.Models.Account.AccountModel");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("UserModel");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVCryptoModel", b =>
                {
                    b.HasBaseType("CurrencyExchangeLibrary.Models.OHLC.OHLCCryptoModel");

                    b.Property<int?>("CryptoModelID")
                        .HasColumnType("int");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("CryptoModelID");

                    b.HasDiscriminator().HasValue("OHLCVCryptoModel");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Account.AdminModel", b =>
                {
                    b.HasBaseType("CurrencyExchangeLibrary.Models.Account.UserModel");

                    b.Property<long>("phone")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("AdminModel");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Crypto.CryptoDataModel", "MetaData")
                        .WithMany()
                        .HasForeignKey("MetaDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Currency.CurrencyModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Currency.CurrencyDataModel", "MetaData")
                        .WithMany()
                        .HasForeignKey("MetaDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCCurrencyModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Currency.CurrencyModel", null)
                        .WithMany("OHLCData")
                        .HasForeignKey("CurrencyModelID");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVStockModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Stock.StockModel", null)
                        .WithMany("OHLCVData")
                        .HasForeignKey("StockModelID");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Stock.StockModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Stock.StockDataModel", "MetaData")
                        .WithMany()
                        .HasForeignKey("MetaDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVCryptoModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", null)
                        .WithMany("OHLCVCryptoData")
                        .HasForeignKey("CryptoModelID");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", b =>
                {
                    b.Navigation("OHLCVCryptoData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Currency.CurrencyModel", b =>
                {
                    b.Navigation("OHLCData");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Stock.StockModel", b =>
                {
                    b.Navigation("OHLCVData");
                });
#pragma warning restore 612, 618
        }
    }
}
