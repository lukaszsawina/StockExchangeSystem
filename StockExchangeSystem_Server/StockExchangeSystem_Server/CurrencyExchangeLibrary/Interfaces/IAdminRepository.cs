using CurrencyExchangeLibrary.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface IAdminRepository
    {
        //Get
        Task<List<AdminModel>> GetAdminsAsync();
        Task<AdminModel> GetAdminAsync(int id);
        Task<AdminModel> GetAdminAsync(string email);
        Task<bool> IsAdmin(int id);

        //Post
        Task<bool> CreateAdminAsync(AdminModel admin);

        //Put
        Task<bool> UpdateAdminAsync(AdminModel admin);
        Task<bool> DeleteAdminAsync (AdminModel admin);
        Task<bool> SaveAsync();

    }
}
