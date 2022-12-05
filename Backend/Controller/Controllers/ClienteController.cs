using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers;

using Exceptions;
using Model;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpGet]
    [Route("GetAllClients")]
    public async Task<IActionResult> GetAllClients()
    {
        object[] clientes;

        try
        {
            clientes = (await Views
                .GetAllClientesAsync())
                .Select(c => new
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .ToArray();
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(clientes);
    }
}
