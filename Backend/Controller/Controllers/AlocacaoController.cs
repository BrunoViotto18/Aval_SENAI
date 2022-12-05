using Microsoft.AspNetCore.Mvc;
using Model.Enums;
using Model;

namespace Controller.Controllers;

using Controller.DTO;
using Exceptions;


[ApiController]
[Route("[controller]")]
public class AlocacaoController : ControllerBase
{
    [HttpGet]
    [Route("GetAllAllocatedAreas")]
    public async Task<IActionResult> GetAllAllocatedAreas()
    {
        Area[] situation;

        try
        {
            situation = await Views
                .GetAllAllocatedAreasAsync();
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(situation);
    }

    [HttpGet]
    [Route("GetAllAutomobilesAllocatedInArea/{area}")]
    public async Task<IActionResult> GetAllAutomobilesAllocatedInArea(Area area)
    {
        object[] automoveis;

        try
        {
            automoveis = (await Views
                .GetAllAutomobilesInAreaAsync(area))
                .Select(auto => new
                {
                    Id = auto.Id,
                    Modelo = auto.Modelo,
                    Preco = auto.Preco
                })
                .ToArray();
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(automoveis);
    }
}
