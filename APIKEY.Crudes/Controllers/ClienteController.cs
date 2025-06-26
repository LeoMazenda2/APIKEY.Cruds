using APIKEY.Crudes.Models;
using APIKEY.Crudes.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIKEY.Crudes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _repo;

    public ClienteController(IClienteRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var cliente = await _repo.GetByIdAsync(id);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Cliente cliente)
    {
        await _repo.AddAsync(cliente);
        await _repo.SaveAsync();
        return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
    {
        var existente = await _repo.GetByIdAsync(id);
        if (existente is null) return NotFound();

        existente.Nome = cliente.Nome;
        _repo.Update(existente);
        await _repo.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _repo.GetByIdAsync(id);
        if (cliente is null) return NotFound();

        _repo.Delete(cliente);
        await _repo.SaveAsync();
        return NoContent();
    }
}
