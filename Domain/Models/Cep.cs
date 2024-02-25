using System.Text.Json.Serialization;

namespace Domain.Models;
public class Cep: CepBase
{
    [JsonPropertyName("logradouro")]
    public required string Logradouro { get; init; }
    [JsonPropertyName("complemento")]
    public required string Complemento { get; init; }
    [JsonPropertyName("bairro")]
    public required string Bairro { get; init; }
    [JsonPropertyName("localidade")]
    public required string Localidade { get; init; }
    [JsonPropertyName("uf")]
    public required string Uf { get; init; }
    [JsonPropertyName("ibge")]
    public required int Ibge { get; init; }
    [JsonPropertyName("gia")]
    public required int Gia { get; init; }
    [JsonPropertyName("ddd")]
    public required int Ddd { get; init; }
    [JsonPropertyName("siafi")]
    public required int Siafi { get; init; }
}
