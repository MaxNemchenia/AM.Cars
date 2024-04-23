using MediatR;

namespace AM.Cars.Client.Application.Mediatr.Commands;

public class DeleteCheckedCommand : IRequest<Unit>
{
    public IEnumerable<long> Ids { get; set; }
}
