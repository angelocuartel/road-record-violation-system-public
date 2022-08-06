using FluentValidation;
using RoadRecordViolationSystem.Validators.CustomValidator;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Validators
{
    public class ChangeAdminPasswordViewModelServerValidator : AbstractValidator<ChangeAddPasswordViewModel>
    {
        public ChangeAdminPasswordViewModelServerValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Should not be empty")
                .MinimumLength(8)
                .WithMessage("Your password is too weak");

            RuleFor(c => c.ConfirmPassword)
                    .NotEmpty()
                    .WithMessage("Should not be empty")
                    .Equal(p => p.Password)
                    .WithMessage("Not equal to password");

            RuleFor(a => a.AdminPassword)
                    .NotEmpty()
                    .WithMessage("Please provide your password");
        }
    }
}
