using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Account
{
    public class UserModel : AccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
