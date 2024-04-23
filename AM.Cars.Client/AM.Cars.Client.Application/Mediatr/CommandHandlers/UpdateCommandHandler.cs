using AM.Cars.Client.Application.Builders.Interfaces;
using AM.Cars.Client.Application.Mediatr.Commands;
using AM.Cars.Client.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace AM.Cars.Client.Application.Mediatr.CommandHandlers;

public class UpdateCommandHandler : BaseHandler<UpdateCommand, Unit>
{
    public UpdateCommandHandler(IHttpMessageBuilder httpMessageBuilder, IOptions<ApiOptions> apiOptions)
        : base(httpMessageBuilder, apiOptions)
    {
    }

    /// <inheritdoc />
    protected override HttpMethod HttpMethod => HttpMethod.Put;
}
