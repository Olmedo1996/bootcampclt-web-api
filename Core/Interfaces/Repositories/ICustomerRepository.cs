using Core.DTOs;
using Core.Entities;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> List(PaginationRequest request, CancellationToken cancellationToke);
    Task Add(CustomerCreateDTO customer);
    Task<CustomerDTO?> GetById(int id);
    Task<bool> Delete(int id);
    Task<bool> Update(CustomerUpdateDTO customer);
}
