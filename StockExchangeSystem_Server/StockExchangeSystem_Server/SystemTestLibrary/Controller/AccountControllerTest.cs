using CurrencyExchangeLibrary.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using StockExchangeSystem_Server.Controllers;

namespace SystemTestLibrary.Controller
{
    public class AccountControllerTest
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountControllerTest()
        {
            _accountRepository = A.Fake<IAccountRepository>();
            _logger = A.Fake<ILogger<AccountController>>();
        }


        //Test sprawdzający czy controller zawsze zwraca true/false, a nie null
        [Fact]
        public void AccountController_GetAccountExistAsync_ReturnOK()
        {
            //Arrange
            string email = "email";            
            var controller = new AccountController(_accountRepository, _logger);

            //Act
            var result = controller.GetAccountExistAsync(email);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
