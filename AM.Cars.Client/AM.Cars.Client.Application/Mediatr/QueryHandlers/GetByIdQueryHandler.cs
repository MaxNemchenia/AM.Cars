using AM.Cars.Client.Application.Builders.Interfaces;
using AM.Cars.Client.Application.Mediatr.Queries;
using AM.Cars.Client.Application.Options;
using AM.Cars.Client.Domain.Models;
using Microsoft.Extensions.Options;

namespace AM.Cars.Client.Application.Mediatr.QueryHandlers;

public class GetByIdQueryHandler : BaseQueryHandler<GetByIdQuery, Car>
{
    public GetByIdQueryHandler(IHttpMessageBuilder httpMessageBuilder, IOptions<ApiOptions> apiOptions)
        : base(httpMessageBuilder, apiOptions)
    {
    }
}
