﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }
    public class MajorIndex
    {
        public decimal Price { get; set; }
        public decimal Changes { get; set; }
        public MajorIndexType Type { get; set; }
    }
}