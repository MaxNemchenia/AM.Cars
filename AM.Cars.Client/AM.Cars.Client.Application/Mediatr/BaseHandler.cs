using AM.Cars.Client.Application.Builders.Interfaces;
using AM.Cars.Client.Application.Extensions;
using AM.Cars.Client.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace AM.Cars.Client.Application.Mediatr;

public abstract class BaseHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
{
    private readonly IHttpMessageBuilder _httpMessageBuilder;
    private readonly ApiOptions _apiOptions;

    protected BaseHandler(
        IHttpMessageBuilder httpMessageBuilder,
        IOptions<ApiOptions> apiOptions)
    {
        _httpMessageBuilder = httpMessageBuilder;
        _apiOptions = apiOptions.Value;
    }
    
    protected string BaseUrl { get; }

    protected Uri Url => new(_apiOptions.Url.TrimEnd('/') + GetEndpoint);

    protected virtual string GetEndpoint => "/car";

    protected abstract HttpMethod HttpMethod { get; }

    /// <inheritdoc />
    public async Task<TResult> Handle(
            TRequest request,
            CancellationToken cancellationToken)
    {
        var httpMessage = _httpMessageBuilder.GetHttpMessage(
                Url,
                request,
                HttpMethod);

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(
            httpMessage,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var content = await stream.ToStringAsync();

            throw new Exception(content);
        }

        var result = await stream.DeserializeJsonAsync<TResult>();

        return result;
    }
}
