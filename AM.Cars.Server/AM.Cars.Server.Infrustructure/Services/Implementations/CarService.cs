using AM.Cars.Server.Application.Repositories;
using AM.Cars.Server.Domain.Entities;
using AM.Cars.Server.Infrustructure.Dtos;
using AM.Cars.Server.Infrustructure.Services.Interfaces;
using AM.Cars.Server.Infrustructure.Utilities.Interfaces;
using AutoMapper;

namespace AM.Cars.Server.Infrustructure.Services.Implementations;

public class CarService : ICarService
{
    private readonly IImageUtility _imageUtility;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarService(
        IImageUtility imageUtility,
        ICarRepository carRepository,
        IMapper mapper)
    {
        _imageUtility = imageUtility;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CarDto>> GetAllCars()
    {
        var cars = _carRepository.QueryAsNoTracking();

        var carDtos = _mapper.Map<List<CarDto>>(cars);

        var carDtosWithImages = await Task.WhenAll(carDtos.Select(async carDto =>
        {
            carDto.Image = await _imageUtility.ReadImageFromStorageAsync(carDto.ImagePath);

            return carDto;
        }));

        return carDtos;
    }

    /// <inheritdoc />
    public async Task<CarDto> GetCarById(long id)
    {
        var car = await _carRepository.ReadAsNoTrackingAsync(id);

        if (car == default)
        {
            throw new ArgumentException("Car not found");
        }

        var carDto = _mapper.Map<CarDto>(car);
        carDto.Image = await _imageUtility.ReadImageFromStorageAsync(car.ImagePath);

        return carDto;
    }

    /// <inheritdoc />
    public async Task AddCar(CarDto carDto)
    {
        var car = _mapper.Map<Car>(carDto);

        try
        {
            car.ImagePath = await _imageUtility.SaveImageToVolumeStorageAsync(carDto.Image, carDto.Name);
            await _carRepository.AddAsync(car);
        }
        catch (Exception)
        {
            if (!string.IsNullOrEmpty(car.ImagePath))
            {
                _imageUtility.DeleteImageFromStorage(car.ImagePath);
            }

            throw;
        }
    }

    /// <inheritdoc />
    public async Task UpdateCar(CarDto carDto)
    {
        var car = _mapper.Map<Car>(carDto);
        var oldCar = await _carRepository.ReadAsNoTrackingAsync(car.Id);

        if (oldCar == default)
        {
            throw new InvalidOperationException("Car already hasn't existed");
        }

        try
        {
            car.ImagePath = await _imageUtility.SaveImageToVolumeStorageAsync(carDto.Image, carDto.Name);
            await _carRepository.UpdateAsync(car);
        }
        catch(Exception)
        {

            if (!string.IsNullOrEmpty(car.ImagePath))
            {
                _imageUtility.DeleteImageFromStorage(car.ImagePath);
            }

            throw;
        }

        _imageUtility.DeleteImageFromStorage(oldCar.ImagePath);
    }

    /// <inheritdoc />
    public async Task DeleteCar(long id)
    {
        var car = await _carRepository.ReadAsync(id);

        if (car == default)
        {
            throw new InvalidOperationException("Car already hasn't existed");
        }

        await _carRepository.DeleteAsync(car);
        _imageUtility.DeleteImageFromStorage(car.ImagePath);
    }

    /// <inheritdoc />
    public async Task DeleteCars(IEnumerable<long> ids)
    {
        var cars = _carRepository.Query().Where(c => ids.Contains(c.Id));

        if (!cars.Any())
        {
            throw new InvalidOperationException("Cars already haven't existed");
        }

        await _carRepository.DeleteAsync(cars);

        foreach (var car in cars)
        {
            _imageUtility.DeleteImageFromStorage(car.ImagePath);
        }
    }
}
