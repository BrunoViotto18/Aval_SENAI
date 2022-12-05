using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers;

using Controller.DTO;
using Model.Enums;
using Exceptions;
using Model;


[ApiController]
[Route("[controller]")]
public class ConcessionariaController : ControllerBase
{
    [HttpPost]
    [Route("GetConcessionariasWithModelAllocatedInArea")]
    public async Task<IActionResult>
        GetConcessionariasWithModelAllocatedInArea([FromBody] ModeloAreaDTO body)
    {
        object[] concessionarias;

        try
        {
            concessionarias = (await Views
                .GetConcessionariasWithModelAllocatedInAreaAsync(body.Modelo, (Area)body.Area))
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

        return Ok(concessionarias);
    }
}
