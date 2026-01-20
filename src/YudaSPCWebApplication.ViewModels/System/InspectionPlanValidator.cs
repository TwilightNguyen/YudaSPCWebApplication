using FluentValidation;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class InspectionPlanValidator : AbstractValidator<InspectionPlanCreateRequest>
    {
        public InspectionPlanValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Inspection Plan Name is required.")
                .MaximumLength(100).WithMessage("Inspection Plan Name must not exceed 100 characters.");

            RuleFor(x => x.AreaId)
                .GreaterThan(0).WithMessage("AreaId must be a positive integer.");
        }
    }
}
