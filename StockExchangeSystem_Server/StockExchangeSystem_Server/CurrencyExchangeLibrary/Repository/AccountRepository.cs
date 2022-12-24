using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        //Get
        public async Task<List<AccountModel>> GetAccountsAsync()
        {
            return await _context.Account.ToListAsync();
        }
        public async Task<AccountModel> GetAccountAsync(int id)
        {
            return await _context.Account.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<AccountModel> GetAccountAsync(string email)
        {
            return await _context.Account.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
        public async Task<bool> AccountExistAsync(int id)
        {
            return await _context.Account.AnyAsync(x => x.ID == id);
        }

        public async Task<bool> AccountExistAsync(string email)
        {
            return await _context.Account.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            return await _context.Account.AnyAsync(x => x.Email == email && x.Password == password);
        }

        //Post

        //Put
        public async Task<bool> UpdateAccountAsync(AccountModel account)
        {
            _context.Account.Update(account);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
