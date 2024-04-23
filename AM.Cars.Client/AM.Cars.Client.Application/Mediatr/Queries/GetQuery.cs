using AM.Cars.Client.Domain.Models;
using MediatR;

namespace AM.Cars.Client.Application.Mediatr.Queries;

public class GetQuery : IRequest<IEnumerable<Car>>
{
}
