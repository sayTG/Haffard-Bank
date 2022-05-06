using CardManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Abstractions.IRepositories
{
    public interface ICustomerRepo
    {
        Customers GetCustomer(string customerId);
    }
}
