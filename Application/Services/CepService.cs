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
        if (cep.Cep.Length != 8) return ResponseHelper.BadRequestResponse<Cep>("CEP inválido!");
        try
        {
            var cepVal = await _repository.GetCep(cep.Cep);
            if (cepVal != null) return ResponseHelper.ConflictResponse<Cep>("CEP já cadastrado!");
        } catch (Exception)
        {
            return ResponseHelper.InternalServerErrorResponse<Cep>("Falha ao obter conexão com o banco de dados!");
        }
        
        using var client = _clientFactory.CreateClient();
        var response = await client.GetAsync($"https://viacep.com.br/ws/{cep.Cep}/json/");

        // Viacep retorna um json com a chave "erro" e status 200 quando o CEP não é encontrado
        var json = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) return ResponseHelper.InternalServerErrorResponse<Cep>("Falha ao obter os dados do CEP!");
        if (json.Contains("erro")) return ResponseHelper.NotFoundResponse<Cep>("CEP não encontrado!");
        
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
        catch (Exception)
        {
            return ResponseHelper.InternalServerErrorResponse<Cep>("Falha ao adicionar o CEP!");
        }
        return ResponseHelper.SuccessCreatedResponse(address, "CEP adicionado com sucesso!");
    }

    public async Task<CepResponse<Cep>> GetCep(string cep)
    {
        if (cep.Length != 8) return ResponseHelper.BadRequestResponse<Cep>("CEP inválido!");
        try
        {
            var cepVal = await _repository.GetCep(cep);
            if (cepVal != null) return ResponseHelper.SuccessResponse(cepVal, "CEP obtido com sucesso!");
        } catch (Exception)
        {
            return ResponseHelper.InternalServerErrorResponse<Cep>("Falha ao obter conexão com o banco de dados!");
        }
        return ResponseHelper.NotFoundResponse<Cep>("CEP não encontrado!");
    }
}
