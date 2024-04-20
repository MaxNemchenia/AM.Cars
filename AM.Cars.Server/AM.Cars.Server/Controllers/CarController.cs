using AM.Cars.Server.Domain.Entities;
using AM.Cars.Server.Infrustructure.Dtos;
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

        return Ok();
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
    public async Task<IActionResult> CreateCar([FromBody] CarDto car)
    {
        await _carService.CreateAsync(car);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCar([FromBody] CarDto car)
    {
        await _carService.UpdateAsync(car);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(long id)
    {
        await _carService.DeleteAsync(id);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCars(IEnumerable<long> ids)
    {
        await _carService.DeleteAsync(ids);

        return NoContent();
    }
}
