using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/*INYECCION DE DEPENDENCIA*/
public class CustomerController: BaseApiController
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        return Ok(_customerRepository.List());
    }

    [HttpPost]
    public IActionResult Add([FromBody] Customer customer)
    {
        _customerRepository.Add(customer);
        return CreatedAtAction(nameof(List), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Customer customer)
    {
        customer.Id = id;
        var updated = _customerRepository.Update(customer);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        var customer = _customerRepository.GetById(id);

        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _customerRepository.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
