using CardManagementAPI.Abstractions;
using CardManagementAPI.Abstractions.IRepositories;
using CardManagementAPI.EntityConfiguration;
using CardManagementAPI.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new CustomerRepo(_context);
        }
        public ICustomerRepo Customers { get; set; }
        public int SaveToDB()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
