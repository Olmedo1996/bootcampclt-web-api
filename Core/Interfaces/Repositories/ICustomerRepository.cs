using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    List<Customer> List();
    void Add(Customer customer);
    Customer? GetById(int id);
    bool Delete(int id);
    bool Update(Customer customer);
}
