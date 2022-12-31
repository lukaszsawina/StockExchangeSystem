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
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAdminAsync(AdminModel admin)
        {
            await _context.Admin.AddAsync(admin);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAdminAsync(AdminModel admin)
        {
            _context.Admin.Remove(admin);
            return await SaveAsync();
        }

        public async Task<AdminModel> GetAdminAsync(int id)
        {
            return await _context.Admin.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<AdminModel> GetAdminAsync(string email)
        {
            return await _context.Admin.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<AdminModel>> GetAdminsAsync()
        {
            return await _context.Admin.ToListAsync();
        }

        public async Task<bool> IsAdmin(int id)
        {
            return await _context.Admin.AnyAsync(x => x.ID == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateAdminAsync(AdminModel admin)
        {
            _context.Update(admin);
            return await SaveAsync();
        }
    }
}
