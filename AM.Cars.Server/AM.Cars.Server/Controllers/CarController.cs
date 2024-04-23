using AM.Cars.Server.Infrustructure.Dtos;
using AM.Cars.Server.Infrustructure.Requests;
using AM.Cars.Server.Infrustructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AM.Cars.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
        => _carService = carService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
    {
        var cars = await _carService.GetAllAsync();

        if (!cars.Any())
        {
            return NotFound();
        }

        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarDto>> GetCar(long id)
    {
        var car = await _carService.GetAsync(id);

        if(car == default)
        {
            return NotFound();
        }

        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody]CreateRequest createRequest)
    {
        await _carService.CreateAsync(createRequest.Car);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCar([FromBody] UpdateRequest updateRequest)
    {
        await _carService.UpdateAsync(updateRequest.Car);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCar([FromBody]DeleteRequest deleteRequest)
    {
        await _carService.DeleteAsync(deleteRequest.Id);

        return NoContent();
    }

    [HttpDelete("delete-batch")]
    public async Task<IActionResult> DeleteCars(IEnumerable<long> ids)
    {
        await _carService.DeleteAsync(ids);

        return NoContent();
    }
}
