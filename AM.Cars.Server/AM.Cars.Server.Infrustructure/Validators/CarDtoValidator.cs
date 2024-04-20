using AM.Cars.Server.Infrustructure.Dtos;
using FluentValidation;

namespace AM.Cars.Server.Infrustructure.Validators;

public class CarDtoValidator : AbstractValidator<CarDto>
{
    public CarDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255).WithMessage("Name length cannot exceed 255 characters");

        RuleFor(x => x.Image).NotNull().WithMessage("Image is required");
    }
}
