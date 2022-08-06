using FluentValidation;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Validators
{
    public class AccountListViewModelClientSideValidation : AbstractValidator<AccountListViewModel>
    {
        public AccountListViewModelClientSideValidation()
        {
            RuleFor(r => r.Role)
                  .NotEmpty()
                  .WithMessage("Please select a role for user");
        }
    }
}
