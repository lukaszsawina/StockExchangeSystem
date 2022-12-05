using CurrencyExchangeLibrary.Models.Account;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.OHLC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }

        //Crypto
        public DbSet<CryptoModel> Crypto { get; set; }
        public DbSet<OHLCModel> OHLCData { get; set; }
        public DbSet<OHLCVModel> OHLCVData { get; set; }
        public DbSet<CryptoDataModel> CryptoData { get; set; }
        //Account
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoDataModel>().HasKey("ID");
            modelBuilder.Entity<CryptoModel>().HasKey("ID");
            modelBuilder.Entity<OHLCModel>().HasKey(u => new
            {
                u.Symbol,
                u.Time
            });
        }
    }
}
