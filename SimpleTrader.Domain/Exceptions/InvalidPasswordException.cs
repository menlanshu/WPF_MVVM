using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        private readonly string _username;
        private readonly string _password;

        public InvalidPasswordException(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public InvalidPasswordException(string message, string username, string password) : base(message)
        {
            _username = username;
            _password = password;
        }

        public InvalidPasswordException(string message, Exception innerException, string username, string password) : base(message, innerException)
        {
            _username = username;
            _password = password;
        }
    }
}
