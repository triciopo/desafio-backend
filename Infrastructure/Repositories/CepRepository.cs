using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CepRepository : ICepRepository
{
    private readonly DapperDbContext _context;
    
    public CepRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<Cep> GetCep(string cep)
    {
        var query = $"SELECT * FROM cep WHERE cep = '{cep}'";
        using (var connection = _context.CreateConnection())
        {
            var cepList = await connection.QueryAsync<Cep>(query);
            return cepList.FirstOrDefault();
        }
    }

    public async Task<Cep> AddCep(Cep cep)
    {
        var query = "INSERT INTO cep(cep, logradouro, complemento, bairro," +
                       "localidade, uf, ibge, gia, ddd, siafi) values (@cep, @logradouro, " +
                       "@complemento, @bairro, @localidade, @uf, @ibge, @gia, @ddd, @siafi)";
        
        var parameters = new DynamicParameters();
        parameters.Add("cep", cep.Cep, DbType.String);
        parameters.Add("logradouro", cep.Logradouro, DbType.String);
        parameters.Add("complemento", cep.Complemento, DbType.String);
        parameters.Add("bairro", cep.Bairro, DbType.String);
        parameters.Add("localidade", cep.Localidade, DbType.String);
        parameters.Add("uf", cep.Uf, DbType.String);
        parameters.Add("ibge", cep.Ibge, DbType.Int32);
        parameters.Add("gia", cep.Gia, DbType.Int32);
        parameters.Add("ddd", cep.Ddd, DbType.Int32);
        parameters.Add("siafi", cep.Siafi, DbType.Int32);

        using (var connectin = _context.CreateConnection())
        {
            await connectin.ExecuteAsync(query, parameters);
        }

        return cep;
    }
    
}