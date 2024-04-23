using AM.Cars.Client.Application.Builders.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AM.Cars.Client.Application.Builders.Implementations;

public class HttpMessageBuilder : IHttpMessageBuilder
{
    /// <inheritdoc />
    public HttpRequestMessage GetHttpMessage<TRequest>(
        Uri endpoint,
        TRequest request,
        HttpMethod httpMethod)
    {
        var httpMessage = new HttpRequestMessage(httpMethod, endpoint)
        {
            Headers = { { HttpRequestHeader.Accept.ToString(), "application/json" } },
            Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json")
        };

        return httpMessage;
    }
}
