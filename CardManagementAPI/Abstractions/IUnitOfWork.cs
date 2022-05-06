using CardManagementAPI.Abstractions.IRepositories;
using System;

namespace CardManagementAPI.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepo Customers { get; }
        int SaveToDB();
    }
}
