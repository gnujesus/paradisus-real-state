using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.TypePropertyF.Commands;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Validators
{
    public sealed class CreateTypePropertyCommandCommandValidator : AbstractValidator<CreateTypePropertyCommand>
    {
        public CreateTypePropertyCommandCommandValidator()
        {
            RuleFor(c => c.TypeProperty.Name).NotEmpty().MaximumLength(60);

            RuleFor(c => c.TypeProperty.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<CreateTypePropertyCommand> context)
        {
            return context.InstanceToValidate.TypeProperty is null
                ? new ValidationResult(new[] { new ValidationFailure("TypePropertyForCreationDto",
                "TypePropertyForCreationDto object is null") })
                : base.Validate(context);
        }
    }
}
