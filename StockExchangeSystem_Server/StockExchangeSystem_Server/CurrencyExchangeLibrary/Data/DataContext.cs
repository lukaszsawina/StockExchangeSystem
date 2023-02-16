using Microsoft.EntityFrameworkCore;
using CurrencyExchangeLibrary.Models.Account;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.Stock;

namespace CurrencyExchangeLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //Crypto
        public DbSet<CryptoModel> Crypto { get; set; }
        public DbSet<OHLCCryptoModel> OHLCCryptoData { get; set; }
        public DbSet<OHLCVCryptoModel> OHLCVCryptoData { get; set; }
        public DbSet<CryptoDataModel> CryptoData { get; set; }
        //Currency
        public DbSet<CurrencyModel> Currency { get; set; }
        public DbSet<CurrencyDataModel> CurrencyData { get; set; }
        public DbSet<OHLCCurrencyModel> OHLCCurrenciesData { get; set; }
        //Stock
        public DbSet<StockModel> Stock { get; set; }
        public DbSet<StockDataModel> StockData { get; set; }
        public DbSet<OHLCVStockModel> OHLCVStockData { get; set; }
        //Account
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<AdminModel> Admin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoDataModel>().HasKey("ID");
            modelBuilder.Entity<CryptoModel>().HasKey("ID");
            modelBuilder.Entity<OHLCCryptoModel>().HasKey(u => new
            {
                u.Symbol,
                u.Time
            });
            modelBuilder.Entity<CurrencyDataModel>().HasKey("ID");
            modelBuilder.Entity<CurrencyModel>().HasKey("ID");
            modelBuilder.Entity<OHLCCurrencyModel>().HasKey(u => new
            {
                u.Symbol,
                u.Time
            });
            modelBuilder.Entity<StockModel>().HasKey("ID");
            modelBuilder.Entity<StockDataModel>().HasKey("ID");
            modelBuilder.Entity<OHLCVStockModel>().HasKey(u => new
            {
                u.Symbol,
                u.Time
            });

            modelBuilder.Entity<AccountModel>().HasKey("ID");

        }
    }
}
