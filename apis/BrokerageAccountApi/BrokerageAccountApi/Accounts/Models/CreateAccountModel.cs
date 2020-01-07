using BrokerageAccountApi.Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrokerageAccountApi.Accounts.Models
{
    public class CreateAccountModel
    {
        public int ClientId { get; set; }

        public String Description { get; set; }

        //public String AccountType { get; set; }

        public decimal? InitialDeposit { get; set; }

        public List<InitialAccountPositionModel> InitialPositions { get; set; }

        public class InitialAccountPositionModel
        {

            public String Ticker { get; set; }

            public decimal Shares { get; set; }

        }

    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class CreateAccountModelValidator : AbstractValidator<CreateAccountModel>
    {

        public CreateAccountModelValidator()
        {
            RuleFor(c => c.ClientId)
                .NotEmpty().WithMessage("Client id must be provided");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Account description must be provided")
                .MaximumLength(InvestmentAccount.ACCOUNT_NAME_MAX_LENGTH).WithMessage($"Account name must be {InvestmentAccount.ACCOUNT_NAME_MAX_LENGTH} characters or less")
                .Matches(InvestmentAccount.ACCOUNT_NAME_VALIDATION, RegexOptions.IgnoreCase).WithMessage("Account name can only contain letters, spaces, hyphens or periods");

            RuleFor(c => c.InitialDeposit)
                .NotEmpty().When(c => c.InitialPositions is null || c.InitialPositions.Count == 0)
                .WithMessage("Either an initial deposit must be provided or a set of initial positions");

            RuleFor(c => c.InitialPositions)                
                .Empty().When(c => c.InitialDeposit.HasValue)
                .WithMessage("Either an initial deposit must be provided or a set of initial positions but not both");

            RuleForEach(c => c.InitialPositions)
                .Must(p => p.Shares > 0).WithMessage($"Shares must be greater than 0 ")
                .When(c => c.InitialPositions.Count > 0);

            
            


        }

    }

}
