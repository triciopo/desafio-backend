using System.Net;
using JetBrains.Annotations;

namespace Application.Responses;

public class CepResponse<T> where T : class?
{
    public HttpStatusCode StatusCode { get; init; }
    public required string Message { [UsedImplicitly] get; set; }
    public T? Data { [UsedImplicitly] get; set; }
}