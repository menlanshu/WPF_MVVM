﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string UserName { get; set; }

        public UserNotFoundException(string userName = null)
        {
            UserName = userName;
        }

        public UserNotFoundException(string message, string userName = null) : base(message)
        {
            UserName = userName;
        }

        public UserNotFoundException(string message, Exception innerException, string userName) : base(message, innerException)
        {
            UserName = userName;
        }
    }
}
