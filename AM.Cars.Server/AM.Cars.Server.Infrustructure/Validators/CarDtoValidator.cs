using FluentValidation;

namespace AM.Cars.Server.Infrustructure.Validators;

public class CarDtoValidator : CarPostDtoValidator
{
    public CarDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required");
    }
}
