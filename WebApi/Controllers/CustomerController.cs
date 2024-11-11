using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Requests;
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
    public async Task<IActionResult> List([FromQuery] PaginationRequest request, CancellationToken cancellationToke)
    {
        var page =  request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;
        var customers = await _customerRepository.List(request, cancellationToke);
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerCreateDTO request)
    {
        //await _customerRepository.Add(customerDto);
        //return CreatedAtAction(nameof(Get), new { id = customerDto.Id }, customerDto);
        await _customerRepository.Add(request);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDTO request)
    {
        if (id != request.Id)
            return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

        var result = await _customerRepository.Update(request);
        if (!result) return NotFound();

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
