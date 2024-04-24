using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Application.Mediatr.Commands;
using AM.Cars.Client.Application.Mediatr.Queries;
using AM.Cars.Client.Domain.Models;
using MediatR;

namespace AM.Cars.Client.Application.ApiAdapters.Implementations;

public class CarApiAdapter : ICarApiAdapter
{
    private readonly IMediator _mediator;

    public CarApiAdapter(IMediator mediator)
        => _mediator = mediator;

    /// <inheritdoc />
    public Task<IEnumerable<Car>> GetAsync()
        => _mediator.Send(new GetQuery());

    /// <inheritdoc />
    public Task CreateAsync(Car car)
        => _mediator.Send(new CreateCommand {  Car = car });

    /// <inheritdoc />
    public Task UpdateAsync(Car car)
        => _mediator.Send(new UpdateCommand {  Car = car });

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var command = new DeleteCommand { Id = id };
        await _mediator.Send(command);
    }

    /// <inheritdoc />
    public async Task DeleteCheckedAsync(IEnumerable<long> ids)
    {
        var command = new DeleteCheckedCommand { Ids = ids };
        await _mediator.Send(command);
    }
}
