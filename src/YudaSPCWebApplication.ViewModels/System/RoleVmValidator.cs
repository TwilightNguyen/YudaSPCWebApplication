using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class RoleVmValidator: AbstractValidator<RoleVm>
    {
        public RoleVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(256).WithMessage("Role name must not exceed 256 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(512).WithMessage("Description must not exceed 512 characters.");

            RuleFor(x => x.IntRoleUser)
                .GreaterThanOrEqualTo(0).WithMessage("IntRoleUser must be non-negative.");

            RuleFor(x => x.IntLevel)
                .GreaterThanOrEqualTo(0).WithMessage("IntLevel must be non-negative.");
        }
    }
}
