using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using StockExchangeSystem_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTestLibrary.Controller
{
    public class UserControllerTest
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserControllerTest()
        {
            _accountRepository = A.Fake<IAccountRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _logger = A.Fake<ILogger<UserController>>();
        }

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("test@test")]
        [InlineData("JanKowal@wp.pl")]
        [InlineData("EwaNowak@haha.pl")]
        public void UserController_ValidUserEmail_ShouldReturnTrue(string email)
        {
            //Arrange
            var controller = new UserController(_userRepository, _accountRepository, _logger);

            //Act
            var result = controller.ValidEmail(email);

            //Assert
            result.Should().BeTrue();
        }

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("testtest")]
        [InlineData("JanKowalwp.pl")]
        [InlineData("EwaNowakhaha.pl")]
        public void UserController_ValidUserEmail_ShouldReturnFalse(string email)
        {
            //Arrange
            var controller = new UserController(_userRepository, _accountRepository, _logger);

            //Act
            var result = controller.ValidEmail(email);

            //Assert
            result.Should().BeFalse();
        }

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("haslo123")]
        [InlineData("Ha slo wo")]
        public void UserController_ValidUserPassword_ShouldReturnTrue(string pass)
        {
            //Arrange
            var controller = new UserController(_userRepository, _accountRepository, _logger);

            //Act
            var result = controller.ValidPassword(pass);

            //Assert
            result.Should().BeTrue();
        }

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("")]
        [InlineData("pass")]
        public void UserController_ValidUserPassword_ShouldReturnFalse(string pass)
        {
            //Arrange
            var controller = new UserController(_userRepository, _accountRepository, _logger);

            //Act
            var result = controller.ValidPassword(pass);

            //Assert
            result.Should().BeFalse();
        }
    }
}
