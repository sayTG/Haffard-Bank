using CardManagementAPI.Abstractions.IRepositories;
using CardManagementAPI.EntityConfiguration;
using CardManagementAPI.Models;
using System.Linq;

namespace CardManagementAPI.Implementations.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private AppDbContext _context;

        public CustomerRepo(AppDbContext context)
        {
            _context = context;
        }
        public Customers GetCustomer(string customerId)
        {
            return _context.Customers
                .Where(x => x.CustomerId == customerId)
                .FirstOrDefault();
        }
    }
}
