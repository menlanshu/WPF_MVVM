using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string UserName { get; }
        public string Password { get; }

        public InvalidPasswordException(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public InvalidPasswordException(string message, string username, string password) : base(message)
        {
            UserName = username;
            Password = password;
        }

        public InvalidPasswordException(string message, Exception innerException, string username, string password) : base(message, innerException)
        {
            UserName = username;
            Password = password;
        }
    }
}
