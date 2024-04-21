using AM.Cars.Server.Infrustructure.Dtos;
using FluentValidation;

namespace AM.Cars.Server.Infrustructure.Validators;

public class CarPostDtoValidator : AbstractValidator<CarDto>
{
    public CarPostDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255).WithMessage("Name length cannot exceed 255 characters");

        RuleFor(x => x.Image).NotNull().WithMessage("Image is required");
    }
}
