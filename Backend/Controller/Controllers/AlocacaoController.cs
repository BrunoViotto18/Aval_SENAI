using Microsoft.AspNetCore.Mvc;
using Model.Enums;
using Model;

namespace Controller.Controllers;

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
            situation = await Views.GetAllAllocatedAreas();
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(situation);
    }

    [HttpGet]
    [Route("GetAllAutomobilesInArea/{area}")]
    public async Task<IActionResult> GetAllAutomobilesInArea(Area area)
    {
        Automovel[] automoveis;

        try
        {
            automoveis = await Views.GetAllAutomobilesInArea(area);
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(automoveis);
    }
}
