using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services
{
    // When you want to add a service, you should add an interface for the service
    // And implement all the stuff or logic in another indepentant libarary to make it loose couple
    // And much easier to extend
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    }
}
