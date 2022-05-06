using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Abstractions
{
    public interface IServices
    {
        Task<int?> GeneratePinAsync(string customerId);
    }
}
