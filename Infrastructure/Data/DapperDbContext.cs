using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Data;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionstring;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionstring = _configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionstring);
}