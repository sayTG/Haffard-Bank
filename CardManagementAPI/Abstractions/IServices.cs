using CardManagementAPI.Models.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Abstractions
{
    public interface IServices
    {
        Task<int?> GeneratePinAsync(string customerId);
        Task<string> ActivateCardAsync(CardDTO cardDTO);
        Task<string> DeactivateCardAsync(CardDTO cardDTO);
        void AddCustomer(CustomerDTO customerDTO);
    }
}
