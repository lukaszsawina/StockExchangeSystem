﻿// <auto-generated />
using System;
using CurrencyExchangeLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CurrencyExchangeLibrary.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCModel", b =>
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

                    b.ToTable("OHLCData");

                    b.HasDiscriminator<string>("Discriminator").HasValue("OHLCModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Account.UserModel", b =>
                {
                    b.HasBaseType("CurrencyExchangeLibrary.Models.Account.AccountModel");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("UserModel");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVModel", b =>
                {
                    b.HasBaseType("CurrencyExchangeLibrary.Models.OHLC.OHLCModel");

                    b.Property<int?>("CryptoModelID")
                        .HasColumnType("int");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("CryptoModelID");

                    b.HasDiscriminator().HasValue("OHLCVModel");
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

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.OHLC.OHLCVModel", b =>
                {
                    b.HasOne("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", null)
                        .WithMany("OHLCVData")
                        .HasForeignKey("CryptoModelID");
                });

            modelBuilder.Entity("CurrencyExchangeLibrary.Models.Crypto.CryptoModel", b =>
                {
                    b.Navigation("OHLCVData");
                });
#pragma warning restore 612, 618
        }
    }
}
