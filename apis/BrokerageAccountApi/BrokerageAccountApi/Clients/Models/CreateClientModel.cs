using AutoMapper;
using BrokerageAccountApi.Core.Domain;
using BrokerageAccountApi.Core.Services.Clients;
using FluentValidation;
using Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrokerageAccountApi.Clients
{
    public class CreateClientModel
    {


        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String StateCode { get; set; }

        public String ZipCode { get; set; }

        public String EmailAddress { get; set; }

        public String Phone { get; set; }

        public DateTime DateOfBirth { get; set; }
    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class CreateClientModelValidator : AbstractValidator<CreateClientModel>
    {

        public CreateClientModelValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name must be provided")
                .MaximumLength(30).WithMessage("First name must be 30 characters or less")
                .Matches(Client.FIRST_NAME_VALIDATION, RegexOptions.IgnoreCase).WithMessage("First name must only contain letters and spaces");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name must be provided")
                .MaximumLength(30).WithMessage("Last name must be 30 characters or less")
                .Matches(Client.LAST_NAME_VALIDATION, RegexOptions.IgnoreCase).WithMessage("Last name only contain letters, spaces, hyphens or periods");

            RuleFor(c => c.StreetAddress)
                .NotEmpty().WithMessage("Street Address is required")
                .MaximumLength(40).WithMessage("Street address must be 40 characters or less");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(30).WithMessage("City must be 30 characters or less");

            RuleFor(c => c.StateCode)
                .NotEmpty().WithMessage("State must be provided")
                .Length(2).WithMessage("State code must be exactly 2 characters")
                .Matches(CommonValidations.US_STATES, RegexOptions.IgnoreCase).WithMessage("State code must be a valid USPS state abbreviation");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("Zip code must be provided")
                .Length(5).WithMessage("Zip codes must be 5 characters")
                .Matches(CommonValidations.US_ZIP_CODES).WithMessage("A 5 digit zip code must be provided");

            RuleFor(c => c.EmailAddress)
                .NotEmpty().WithMessage("Email address must be provided")
                .Matches(CommonValidations.EMAIL).WithMessage("Email address must be in the proper format");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number must be provided")
                .Matches(CommonValidations.US_PHONE_NUMBERS).WithMessage("Phone number must be in the format XXX-YYY-ZZZZ");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth must be provided")
                .LessThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("Client must have a date of birth making them 18 years old or older")
                .GreaterThanOrEqualTo(DateTime.Today.AddYears(-100)).WithMessage("Date of birth cannot be more than 100 years ago");

        }

    }



    public class CreateClientModelAutomapperProfile : Profile
    {

        public CreateClientModelAutomapperProfile()
        {
            CreateMap<CreateClientModel, CreateClientCommand>();
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


}
