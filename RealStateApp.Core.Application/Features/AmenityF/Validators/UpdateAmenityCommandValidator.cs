using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.AmenityF.Commands;

namespace RealStateApp.Core.Application.Features.AmenityF.Validators
{
    public sealed class UpdateAmenityCommandValidator : AbstractValidator<UpdateAmenityCommand>
    {
        public UpdateAmenityCommandValidator()
        {
            RuleFor(c => c.Amenity.Name).NotEmpty().MaximumLength(60);

            RuleFor(c => c.Amenity.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<UpdateAmenityCommand> context)
        {
            return context.InstanceToValidate.Amenity is null
                ? new ValidationResult(new[] { new ValidationFailure("AmenityForCreationDto",
                "AmenityForCreationDto object is null") })
                : base.Validate(context);
        }
    }
}
