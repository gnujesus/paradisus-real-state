using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.TypeSaleF.Commands;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Validators
{
    public sealed class CreateTypeSaleCommandCommandValidator : AbstractValidator<CreateTypeSaleCommand>
    {
        public CreateTypeSaleCommandCommandValidator()
        {
            RuleFor(c => c.TypeSale.Name).NotEmpty().MaximumLength(60);

            RuleFor(c => c.TypeSale.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<CreateTypeSaleCommand> context)
        {
            return context.InstanceToValidate.TypeSale is null
                ? new ValidationResult(new[] { new ValidationFailure("TypeSaleForCreationDto",
                "TypeSaleForCreationDto object is null") })
                : base.Validate(context);
        }
    }
}
