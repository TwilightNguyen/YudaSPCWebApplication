using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace YudaSPCWebApplication.ViewModels.System
{
    public class InspectionPlanSubValidator : AbstractValidator<InspectionPlanSubCreateRequest>
    {
        public InspectionPlanSubValidator()
        {
            RuleFor(x => x.InspPlanId).GreaterThan(0).WithMessage("InspPlanId must be greater than 0.");
            RuleFor(x => x.PlanTypeId).GreaterThan(0).WithMessage("PlanTypeId must be greater than 0.");
        }
    }
}
