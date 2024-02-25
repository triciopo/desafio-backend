using System.Text.Json.Serialization;

namespace Domain.Models;

public class CepBase
{
    [JsonPropertyName("cep")]
    public required string Cep { get; init; }
}
    