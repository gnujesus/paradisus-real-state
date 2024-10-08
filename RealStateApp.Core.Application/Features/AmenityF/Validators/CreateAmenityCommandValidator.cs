﻿using FluentValidation;
using FluentValidation.Results;
using RealStateApp.Core.Application.Features.AmenityF.Commands;

namespace RealStateApp.Core.Application.Features.AmenityF.Validators
{
    public sealed class CreateTypeSaleCommandValidator : AbstractValidator<CreateAmenityCommand>
    {
        public CreateTypeSaleCommandValidator()
        {
            RuleFor(c => c.Amenity.Name).NotEmpty().MaximumLength(60);

            RuleFor(c => c.Amenity.Description).NotEmpty().MaximumLength(120);
        }

        public override ValidationResult Validate(ValidationContext<CreateAmenityCommand> context)
        {
            return context.InstanceToValidate.Amenity is null
                ? new ValidationResult(new[] { new ValidationFailure("AmenityForCreationDto",
                "AmenityForCreationDto object is null") })
                : base.Validate(context);
        }
    }
}
