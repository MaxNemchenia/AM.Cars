namespace AM.Cars.Client.Application.Builders.Interfaces;

public interface IHttpMessageBuilder
{
    /// <summary>
    /// Builds an HTTP request message.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request data.</typeparam>
    /// <param name="endpoint">The endpoint for the HTTP request.</param>
    /// <param name="request">The request data to be included in the HTTP message body.</param>
    /// <param name="httpMethod">The HTTP method for the request (e.g., GET, POST, PUT, DELETE).</param>
    HttpRequestMessage GetHttpMessage<TRequest>(
        Uri endpoint,
        TRequest request,
        HttpMethod httpMethod);
}
