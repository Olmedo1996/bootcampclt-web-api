using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Requests;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para listar todos los clientes
    public async Task<List<CustomerDTO>> List(PaginationRequest request, CancellationToken cancellationToken)
    {
        int page = request.Page!.Value;
        int pageSize = request.PageSize!.Value;

        var dtos = await _context.Customers
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .Select(customer => new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                Phone = customer.Phone,
                Email = customer.Email,
                BirthDate = customer.BirthDate,
            })
            .ToListAsync(cancellationToken);

        var count = await _context.Customers.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return dtos;
    }

    // Método para agregar un nuevo cliente
    public async Task Add(CustomerCreateDTO request)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    // Método para obtener un cliente por su ID
    public async Task<CustomerDTO?> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        return new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            City = customer.City,
        };
    }


    public async Task<bool> Update(CustomerUpdateDTO customerDto)
    {
        var customer = await _context.Customers.FindAsync(customerDto.Id);
        if (customer == null) return false;

        customer.Id = customerDto.Id;
        customer.Name = customerDto.Name;
        customer.Address = customerDto.Address;
        customer.City = customerDto.City;

        await _context.SaveChangesAsync();
        return true;
    }

    // Método para eliminar un cliente por su ID
    public async Task<bool> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
