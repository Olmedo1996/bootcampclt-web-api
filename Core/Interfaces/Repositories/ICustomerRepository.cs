using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> List();
    Task Add(CustomerDTO customer);
    Task<CustomerDTO?> GetById(int id);
    Task<bool> Delete(int id);
    Task<bool> Update(CustomerDTO customer);
}
