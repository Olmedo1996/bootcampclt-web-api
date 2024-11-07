using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
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
    public async Task<List<CustomerDTO>> List()
    {
        var entities = await _context.Customers.ToListAsync();
        var dtos = entities.Select(customer => new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            City = customer.City,
        });
        return dtos.ToList();
    }

    // Método para agregar un nuevo cliente
    public async Task Add(CustomerDTO customerDto)
    {
        var customer = new Customer
        {
            Name = customerDto.Name,
            Address = customerDto.Address,
            City = customerDto.City,
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


    public async Task<bool> Update(CustomerDTO customerDto)
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
