using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Securities.Prices.Models
{


    /// <summary>
    /// Represents the parameters that can be passed to the /api/Security/ticker/Prices endpoint.  Done as
    /// a separate class so validation can use FluentValidation
    /// </summary>
    public class SecurityPriceParameters
    {


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }


    public class SecurityPriceParametersValidator : AbstractValidator<SecurityPriceParameters>
    {
        public SecurityPriceParametersValidator()
        {
            // If you have an end date, then you must have a start date
            RuleFor(p => p.StartDate)
                .NotNull()
                .When(p => p.EndDate.HasValue)
                .WithMessage("A start date must be provided when an end date is provided");

            RuleFor(p => p.EndDate)
                .Must((p, endDate) => endDate.Value > p.StartDate.Value)
                .When(p => p.EndDate.HasValue)
                .When(p => p.StartDate.HasValue)
                .WithMessage("The end date must be after the start date");
        }
    }

}
