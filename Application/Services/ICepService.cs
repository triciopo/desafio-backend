using Application.Responses;
using Domain.Models;

namespace Application.Services;

public interface ICepService
{
    Task<CepResponse<Cep>> AddCep(CepBase cep);
    Task<CepResponse<Cep>> GetCep(string cep);
}