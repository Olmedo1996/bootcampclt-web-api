using Core.DTOs;
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
    public async Task<IActionResult> List()
    {
        var customers = await _customerRepository.List();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerDTO customerDto)
    {
        await _customerRepository.Add(customerDto);
        return CreatedAtAction(nameof(Get), new { id = customerDto.Id }, customerDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerDTO customerDto)
    {
        customerDto.Id = id;
        var updated = await _customerRepository.Update(customerDto);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var customer = await _customerRepository.GetById(id);

        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _customerRepository.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
