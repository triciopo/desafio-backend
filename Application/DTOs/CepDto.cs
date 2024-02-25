using System.Text.Json.Serialization;

namespace Application.DTOs;

public class CepDto
{
    // DTO Para receber os dados da @ViaCep
    [JsonPropertyName("cep")]
    public required string Cep { get; init; }
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
    public required string Ibge { get; init; }
    [JsonPropertyName("gia")]
    public required string Gia { get; init; }
    [JsonPropertyName("ddd")]
    public required string Ddd { get; init; }
    [JsonPropertyName("siafi")]
    public required string Siafi { get; init; }
}