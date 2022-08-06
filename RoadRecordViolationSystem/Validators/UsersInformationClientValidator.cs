using FluentValidation;
using RoadRecordViolationSystem.Models;

namespace RoadRecordViolationSystem.Validators.CustomValidator
{
    public class UsersInformationClientValidator : AbstractValidator<UsersInformation>
    {
        public UsersInformationClientValidator()
        {
            RuleFor(g => g.GivenName)
                     .NotNull()
                     .WithMessage("Should not be empty");

            RuleFor(m => m.MiddleName)
                    .NotEmpty()
                    .WithMessage("Should not be empty");

           RuleFor(l => l.LastName)
                   .NotEmpty()
                   .WithMessage("Should not be empty");

            RuleFor(a => a.Address)
                    .NotEmpty()
                    .WithMessage("Should not be empty")
                     .MaximumLength(50);

            RuleFor(c => c.City)
                .NotEmpty()
                .WithMessage("Should not be empty")
                .Length(2, 50)
                .WithMessage("City name length is not valid");

            RuleFor(g => g.Gender)
                 .NotEmpty()
                 .WithMessage("Please select a gender");
        }
    }
}
