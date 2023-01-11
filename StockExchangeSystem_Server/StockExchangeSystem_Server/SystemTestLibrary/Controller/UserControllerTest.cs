using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using CurrencyExchangeLibrary.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using StockExchangeSystem_Server;
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

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("test@test")]
        [InlineData("JanKowal@wp.pl")]
        [InlineData("EwaNowak@haha.pl")]
        public void UserController_ValidEmail_ShouldReturnTrue(string email)
        {
            //Arrange

            //Act
            var result = Validation.ValidEmail(email);

            //Assert
            result.Should().BeTrue();
        }

        //Test sprawdzający czy użytkownik podał poprawny format email
        [Theory]
        [InlineData("testtest")]
        [InlineData("JanKowalwp.pl")]
        [InlineData("EwaNowakhaha.pl")]
        public void UserRepository_ValidEmail_ShouldReturnFalse(string email)
        {
            //Arrange

            //Act
            var result = Validation.ValidEmail(email);

            //Assert
            result.Should().BeFalse();
        }

        //Test sprawdzający czy użytkownik podał poprawny format hasła
        [Theory]
        [InlineData("haslo123")]
        [InlineData("Ha slo wo")]
        public void UserController_ValidPassword_ShouldReturnTrue(string pass)
        {
            //Arrange

            //Act
            var result = Validation.ValidPassword(pass);

            //Assert
            result.Should().BeTrue();
        }

        //Test sprawdzający czy użytkownik podał poprawny format hasła
        [Theory]
        [InlineData("")]
        [InlineData("pass")]
        public void UserController_ValidPassword_ShouldReturnFalse(string pass)
        {
            //Arrange

            //Act
            var result = Validation.ValidPassword(pass);

            //Assert
            result.Should().BeFalse();
        }
    }
}
