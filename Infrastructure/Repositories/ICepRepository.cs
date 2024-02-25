using Domain.Models;

namespace Infrastructure.Repositories;

public interface ICepRepository
{
    Task<Cep> AddCep(Cep cep);
    Task<Cep?> GetCep(string cep);
}