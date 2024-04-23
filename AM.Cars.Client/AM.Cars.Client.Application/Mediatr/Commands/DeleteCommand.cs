using MediatR;

namespace AM.Cars.Client.Application.Mediatr.Commands;

public class DeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
