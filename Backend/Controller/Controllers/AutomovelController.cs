using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers;

using Exceptions;
using Model;

[ApiController]
[Route("[controller]")]
public class AutomovelController : ControllerBase
{
    [HttpGet]
    [Route("GetAutomobileModelFromId/{id}")]
    public async Task<IActionResult> GetAutomobileModelFromId(int id)
    {
        string name;

        try
        {
            name = await Views.GetAutomobileModelFromIdAsync(id);
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok(name);
    }
}
