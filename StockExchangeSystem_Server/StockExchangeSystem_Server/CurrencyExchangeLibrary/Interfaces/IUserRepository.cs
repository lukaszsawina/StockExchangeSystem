using CurrencyExchangeLibrary.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserAsync(int id);
        Task<UserModel> GetUserAsync(string email);
        Task<bool> CreateUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<bool> DeleteUserAsync(UserModel user);
        Task<bool> SaveAsync();
    }
}
