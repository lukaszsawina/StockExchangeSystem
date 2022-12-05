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

        public async Task<bool> AccountExistAsync(int id)
        {
            return await _context.Account.AnyAsync(x => x.ID == id);
        }

        public async Task<bool> AccountExistAsync(string email)
        {
            return await _context.Account.AnyAsync(x => x.Email == email);
        }

        public async Task<AccountModel> GetAccountAsync(int id)
        {
            return await _context.Account.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<AccountModel> GetAccountAsync(string email)
        {
            return await _context.Account.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<AccountModel>> GetAccountsAsync()
        {
            return await _context.Account.ToListAsync();
        }
    }
}
