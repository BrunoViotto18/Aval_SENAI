using Controller.DTO;
using Controller.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Controller.Controllers;


[ApiController]
[Route("[controller]")]
public class VendaController : ControllerBase
{
    [HttpPost]
    [Route("RegisterVenda")]
    public async Task<IActionResult> RegisterVenda([FromBody] VendaDTO venda)
    {
        try
        {
            var alocacaoId = (await Views
                .GetAlocacaoFromForeignKeysAsync(venda.automovelId, venda.concessionariaId))
                .Id;

            await Venda.CreateAsync(venda.clientId, alocacaoId);
        }
        catch (Exception e)
        {
            return BadRequest(new UnexpectedException(e));
        }

        return Ok();
    }
}
