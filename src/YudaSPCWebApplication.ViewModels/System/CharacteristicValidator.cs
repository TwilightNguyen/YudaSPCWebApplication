using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class CharacteristicValidator : AbstractValidator<CharacteristicCreateRequest>
    {
        public CharacteristicValidator() {
           RuleFor(x => x.CharacteristicName)
                .NotEmpty().WithMessage("Characteristic Name is required.")
                .MaximumLength(100).WithMessage("Characteristic Name must not exceed 100 characters.");

            RuleFor(x => x.MeaTypeId)
                .NotNull().WithMessage("MeaTypeId is required.")
                .GreaterThan(0).WithMessage("MeaTypeId must be a positive integer.");

            RuleFor(x => x.ProcessId)
                .NotNull().WithMessage("ProcessId is required.")
                .GreaterThan(0).WithMessage("ProcessId must be a positive integer.");

            RuleFor(x => x.CharacteristicType)
                .NotNull().WithMessage("CharacteristicType is required.")
                .GreaterThan(0).WithMessage("CharacteristicType must be a positive integer.");

            RuleFor(x => x.CharacteristicUnit)
                .MaximumLength(50).WithMessage("Characteristic Unit must not exceed 50 characters.");

            RuleFor(x => x.DefectRateLimit)
                .GreaterThanOrEqualTo(0).WithMessage("Defect Rate Limit must be non-negative.").When(x => x.DefectRateLimit.HasValue);

            RuleFor(x => x.EventEnable)
                .InclusiveBetween(0, 1).WithMessage("Event Enable must be either 0 (disabled) or 1 (enabled).").When(x => x.EventEnable.HasValue);

            RuleFor(x => x.EmailEventModel)
                .NotNull().WithMessage("Email Event Model is required.").GreaterThan(0).WithMessage("Email Event Model must be a positive integer.");

            RuleFor(x => x.Decimals)
                .GreaterThanOrEqualTo(0).WithMessage("Decimals must be non-negative.");


        }
    }
}
