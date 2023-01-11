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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateUserAsync(UserModel user)
        {
            await _context.User.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<bool> DeleteUserAsync(UserModel user)
        {
            _context.User.Remove(user);
            return await SaveAsync();
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            return await _context.User.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserMailAsync(string email)
        {
            return await _context.User.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            _context.User.Update(user);
            return await SaveAsync();
        }
    }
}
