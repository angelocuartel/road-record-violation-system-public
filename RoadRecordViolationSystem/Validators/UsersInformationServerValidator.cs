using FluentValidation;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Validators.CustomValidator;
using RoadRecordViolationSystem.ViewModels;
using System.Linq;

namespace RoadRecordViolationSystem.Validators
{
    public class UsersInformationServerValidator : AbstractValidator<AddUserViewModel>
    {
        public UsersInformationServerValidator(AppDBContext context)
        {
            RuleFor(g => g.UserInfo.GivenName)
                  .Cascade(CascadeMode.Stop)
                  .NotNull()
                  .WithMessage("Should not be empty")
                  .HasNoNumber()
                  .HasNoSymbol();

            RuleFor(m => m.UserInfo.MiddleName)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Should not be empty")
                   .HasNoNumber()
                   .HasNoSymbol();

            RuleFor(l => l.UserInfo.LastName)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage("Should not be empty")
                 .HasNoNumber()
                 .HasNoSymbol();

            RuleFor(a => a.UserInfo.Address)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage("Should not be empty")
                 .HasNoSymbol();

            RuleFor(c => c.UserInfo.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Should not be empty")
                .Length(2, 50)
                .WithMessage("City name length is not valid")
                .HasNoNumber()
                .HasNoSymbol();

            RuleFor(g => g.UserInfo.Gender)
                 .NotEmpty()
                 .WithMessage("Should not be empty");

            RuleFor(u => u.Account.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Should not be empty")
                .Must((m,i) =>  UniqueUserName(m,i,context))
                .WithMessage("Username already exist");

            RuleFor(g => g.Password)
               .NotEmpty()
               .WithMessage("Please provide a password");

            RuleFor(g => g.ConfirmPassword)
             .Equal(p => p.Password)
             .WithMessage("Not equal to password");
        }

        private bool UniqueUserName(AddUserViewModel model, string userName,AppDBContext context)
        {
            var result = context.Users
                .Where(i => i.UsersInfoId != model
                .UserInfo.UserInfoId && i
                .UserName == userName)
                .SingleOrDefault();
            return result is null;
        }
    }
}
