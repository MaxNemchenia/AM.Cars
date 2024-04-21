using AM.Cars.Server.Domain.Entities;
using AM.Cars.Server.Infrustructure.Dtos;
using AutoMapper;

namespace AM.Cars.Server.Infrustructure.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Image, opt => opt.Ignore());

        CreateMap<CarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

        CreateMap<CarPostDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => default(int)))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}
