using System.Net;
using System.Text.Json;
using Domain.Models;
using Application.DTOs;
using Application.Responses;
using Infrastructure.Repositories;

namespace Application.Services;

public class CepService : ICepService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ICepRepository _repository;

    public CepService(IHttpClientFactory clientFactory, ICepRepository repository)
    {
        _clientFactory = clientFactory;
        _repository = repository;
    }

    public async Task<CepResponse<Cep>> AddCep(CepBase cep)
    {
        if (cep.Cep.Length != 8)
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "CEP inválido!",
                Data = null
            };
        }

        try
        {
            var cepVal = await _repository.GetCep(cep.Cep);
            if (cepVal != null)
            {
                return new CepResponse<Cep>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Message = "CEP já cadastrado!",
                    Data = null
                };
            }
        } catch (Exception e)
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Falha ao obter conexão com o banco de dados!",
                Data = null
            };
        }
        
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync($"https://viacep.com.br/ws/{cep.Cep}/json/");

        if (!response.IsSuccessStatusCode)
        {
            return new CepResponse<Cep>
            {
                StatusCode = response.StatusCode,
                Message = "Falha ao obter os dados do CEP.",
                Data = null
            };
        }
        var json = await response.Content.ReadAsStringAsync();
        // Viacep retorna um json com a chave "erro" quando o CEP não é encontrado
        if (json.Contains("erro"))
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "CEP não encontrado!",
                Data = null
            };
        }
        
        var cepDto = JsonSerializer.Deserialize<CepDto>(json);
        var address = new Cep
        {
            Cep = cep.Cep,
            Logradouro = cepDto!.Logradouro,
            Complemento = cepDto.Complemento,
            Bairro = cepDto.Bairro,
            Localidade = cepDto.Localidade,
            Uf = cepDto.Uf,
            Ibge = Convert.ToInt32(cepDto.Ibge),
            Gia = Convert.ToInt32(cepDto.Gia),
            Ddd = Convert.ToInt32(cepDto.Ddd),
            Siafi = Convert.ToInt32(cepDto.Siafi)
        };
        try
        {
            await _repository.AddCep(address);
        }
        catch (Exception e)
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Falha ao obter conexão com o banco de dados!",
                Data = null
            };
        }
        return new CepResponse<Cep>
        {
            StatusCode = HttpStatusCode.Created,
            Message = "CEP adicionado com sucesso!",
            Data = address
        };
    }

    public async Task<CepResponse<Cep>> GetCep(string cep)
    {
        if (cep.Length != 8)
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "CEP inválido!",
                Data = null
            };
        }

        try
        {
            var cepVal = await _repository.GetCep(cep);
            if (cepVal != null)
            {
                return new CepResponse<Cep>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "CEP obtido com sucesso!",
                    Data = cepVal
                };
            }
        } catch (Exception e)
        {
            return new CepResponse<Cep>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Falha ao obter conexão com o banco de dados!",
                Data = null
            };
        }
        
        return new CepResponse<Cep>
        {
            StatusCode = HttpStatusCode.NotFound,
            Message = "CEP não encontrado!",
            Data = null
        };
    }
}
