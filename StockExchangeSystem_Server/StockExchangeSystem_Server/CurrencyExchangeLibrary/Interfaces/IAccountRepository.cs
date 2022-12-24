using CurrencyExchangeLibrary.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface IAccountRepository
    {
        //Get
        Task<List<AccountModel>> GetAccountsAsync();
        Task<AccountModel> GetAccountAsync(int id);
        Task<AccountModel> GetAccountAsync(string email);
        Task<bool> AccountExistAsync(int id);
        Task<bool> AccountExistAsync(string email);
        Task<bool> LoginAsync(string email, string password);

        //Post
        //Put
        Task<bool> UpdateAccountAsync(AccountModel account);

        Task<bool> SaveAsync();

    }
}
