using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.TypeSaleF.Commands;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Validators
{
    public sealed class UpdateTypeSaleCommandValidator : AbstractValidator<UpdateTypeSaleCommand>
    {
        public UpdateTypeSaleCommandValidator()
        {
            RuleFor(c => c.TypeSale.Name).NotEmpty().MaximumLength(60);
            RuleFor(c => c.TypeSale.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<UpdateTypeSaleCommand> context)
        {
            return context.InstanceToValidate.TypeSale is null
                ? new ValidationResult(new[] { new ValidationFailure("TypeSaleForUpdateDto",
                "TypeSaleForUpdateDto object is null") })
                : base.Validate(context);
        }
    }
}
