using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Infrastructure.Repositories;
using System.Text.Json;
using Application.DTOs;

namespace DesafioNegocieOnline.Controllers
{
    [ApiController]
    [Route("api/cep")]
    public class CepController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICepRepository _repository;

        public CepController(IHttpClientFactory clientFactory, ICepRepository repository)
        {
            _clientFactory = clientFactory;
            _repository = repository;
        }

        [HttpPost(Name = "PostCep")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] CepBase cep)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep.Cep}/json/");
            
            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();
                var cepDto = JsonSerializer.Deserialize<CepDto>(json);
                var address = new Cep {
                    Cep = cep.Cep,
                    Logradouro = cepDto.Logradouro,
                    Complemento = cepDto.Complemento,
                    Bairro = cepDto.Bairro,
                    Localidade = cepDto.Localidade,
                    Uf = cepDto.Uf,
                    Ibge = Convert.ToInt32(cepDto.Ibge),
                    Gia = Convert.ToInt32(cepDto.Gia),
                    Ddd = Convert.ToInt32(cepDto.Ddd),
                    Siafi = Convert.ToInt32(cepDto.Siafi)
                };
            
                await _repository.AddCep(address);
                return Ok(address);
            }
            return StatusCode((int)response.StatusCode, "Falha ao obter CEP da API.");
        }

        [HttpGet("{cep}", Name = "GetCep")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Cep> GetAsync(string cep)
        {
            return await _repository.GetCep(cep);
        }
    }
}