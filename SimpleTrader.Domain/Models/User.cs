﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DatedJoined { get; set; }
    }
}
