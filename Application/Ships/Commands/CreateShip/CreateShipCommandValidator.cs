using FluentValidation;

namespace Application.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }

    public class LatLongDtoValidator : AbstractValidator<LatLongDto>
    {
        public LatLongDtoValidator()
        {
            RuleFor(dto => dto.Latitude)
                .NotNull()
                .WithMessage("Latitude is required.");

            RuleFor(dto => dto.Longitude)
                .NotNull()
                .WithMessage("Longitude is required.");
        }
    }
}
