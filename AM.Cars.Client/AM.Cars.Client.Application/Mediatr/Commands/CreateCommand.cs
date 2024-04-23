using AM.Cars.Client.Domain.Models;
using MediatR;

namespace AM.Cars.Client.Application.Mediatr.Commands;

public class CreateCommand : IRequest<Unit>
{
    public Car Car { get; set; }
}
