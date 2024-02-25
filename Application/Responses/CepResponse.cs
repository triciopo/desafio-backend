using System.Net;

namespace Application.Responses;

public class CepResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    
}