using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.TypePropertyF.Commands;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Validators
{
    public sealed class UpdateTypePropertyCommandValidator : AbstractValidator<UpdateTypePropertyCommand>
    {
        public UpdateTypePropertyCommandValidator()
        {
            RuleFor(c => c.TypeProperty.Name).NotEmpty().MaximumLength(60);

            RuleFor(c => c.TypeProperty.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<UpdateTypePropertyCommand> context)
        {
            return context.InstanceToValidate.TypeProperty is null
                ? new ValidationResult(new[] { new ValidationFailure("TypePropertyForUpdateDto",
                "TypePropertyForUpdateDto object is null") })
                : base.Validate(context);
        }
    }
}
