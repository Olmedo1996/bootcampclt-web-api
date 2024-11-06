using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infraestructure.Repositories
{
    public class CustomerReporistory : ICustomerRepository
    {
        private static List<Customer> _customers = [
            new() { Id= 1, Name="Jose", Address="San Lorenzo", City="Asuncion"}, 
            new() { Id= 2, Name="Fernando", Address="San Antonio", City="Luque"},
            new() { Id= 3, Name="Jonh", Address="Jara", City="Capiata"}
        ];

        public List<Customer> List()
        {
            return _customers;
        }

        public void Add(Customer customer)
        {
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id)+1 : 1;
            _customers.Add(customer);
        }

        public bool Update(Customer customer) {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer == null) { 
                return false;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;

            return true;
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public bool Delete(int id) {
            var customer = GetById(id);
            if (customer == null)
            {
                return false;
            }
            _customers.Remove(customer);
            return true;
        }
    }

}
