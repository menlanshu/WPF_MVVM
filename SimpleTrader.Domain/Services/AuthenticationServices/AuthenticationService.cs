using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        // We can use this way to make class dependent inject more and more
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountService _accountDataService;

        public AuthenticationService(IPasswordHasher passwordHasher, IAccountService accountDataService)
        {
            _passwordHasher = passwordHasher;
            _accountDataService = accountDataService;
        }

        public async Task<Account> Login(string username, string password)
        {
            // Get Account from database from username
            Account storedAccount = await _accountDataService.GetByUserName(username);

            if(storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            // Compare password hash with password hash from db
            PasswordVerificationResult passwordVerification = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            // Resurn current account
            if(passwordVerification != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if(password != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            Account emailAccount = await _accountDataService.GetByEmail(email);

            if(emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account usernameAccount = await _accountDataService.GetByUserName(username);

            if(usernameAccount !=null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    UserName = username,
                    PasswordHash = hashedPassword,
                    DatedJoined = DateTime.Now
                };


                Account account = new Account()
                {
                    AccountHolder = user
                };

                await _accountDataService.Create(account);

            }

            return result;
        }
    }
}
