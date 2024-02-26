using System.Net;

namespace Application.Responses;

public class ResponseHelper
{ 
    public static CepResponse<T> BadRequestResponse<T>(string message) where T : class =>
        new() { StatusCode = HttpStatusCode.BadRequest, Message = message };

    public static CepResponse<T> ConflictResponse<T>(string message) where T : class =>
        new() { StatusCode = HttpStatusCode.Conflict, Message = message };

    public static CepResponse<T> NotFoundResponse<T>(string message) where T : class =>
        new() { StatusCode = HttpStatusCode.NotFound, Message = message };

    public static CepResponse<T> InternalServerErrorResponse<T>(string message) where T : class =>
        new() { StatusCode = HttpStatusCode.InternalServerError, Message = message };

    public static CepResponse<T> SuccessResponse<T>(T data, string message) where T : class =>
        new() { StatusCode = HttpStatusCode.OK, Message = message, Data = data };
        
    public static CepResponse<T> SuccessCreatedResponse<T>(T data, string message) where T : class =>
        new() { StatusCode = HttpStatusCode.Created, Message = message, Data = data };
}