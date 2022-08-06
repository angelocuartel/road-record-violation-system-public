using FluentValidation;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(u => u.UserName)
                    .NotEmpty()
                    .WithMessage("Field should not be empty");

            RuleFor(p => p.PassWord)
                    .NotEmpty()
                    .WithMessage("Please provide a password");
        }
    }
}
