using System.Net;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Responses;
using Application.Services;

namespace DesafioNegocieOnline.Controllers;

[ApiController]
[Route("api/cep")]
public class CepController : ControllerBase
{
    private readonly ICepService _service;

    public CepController(ICepService service)
    {
        _service = service;
    }

    [HttpPost(Name = "PostCep")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CepResponse<Cep>>> AddCep([FromBody] CepBase cep)
    {
        var cepResult = await _service.AddCep(cep);
        return cepResult.StatusCode switch
        {
            HttpStatusCode.BadRequest => BadRequest(cepResult),
            HttpStatusCode.Conflict   => Conflict(cepResult),
            HttpStatusCode.Created    => CreatedAtRoute("GetCep", new { cep = cep.Cep }, cepResult),
            _                         => StatusCode(StatusCodes.Status500InternalServerError, cepResult)
        };
    }

    [HttpGet("{cep}", Name = "GetCep")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CepResponse<Cep>>> GetCep(string cep)
    {
        var cepResult = await _service.GetCep(cep);
        return cepResult.StatusCode switch
        {
            HttpStatusCode.BadRequest => BadRequest(cepResult),
            HttpStatusCode.NotFound   => NotFound(cepResult),
            HttpStatusCode.OK         => Ok(cepResult),
            _                         => StatusCode(StatusCodes.Status500InternalServerError, cepResult)
        };
    } 
}