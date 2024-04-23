using AM.Cars.Client.Application.Builders.Interfaces;
using AM.Cars.Client.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace AM.Cars.Client.Application.Mediatr.QueryHandlers;

public abstract class BaseQueryHandler<TRequest, TResponse> : BaseHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    protected BaseQueryHandler(IHttpMessageBuilder httpMessageBuilder, IOptions<ApiOptions> apiOptions)
        : base(httpMessageBuilder, apiOptions)
    {
    }

    /// <inheritdoc />
    protected override HttpMethod HttpMethod => HttpMethod.Get;
}
