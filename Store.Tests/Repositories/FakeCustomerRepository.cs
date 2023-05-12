using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer Get(string document)
    {
        return document == "12345678911" ? new Customer("Vin Diesel", "vin.diesel@hotmail.com") : null;
    }
    
}