using FluentValidation;
using FluentValidation.Validators;
using RoadRecordViolationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Validators.CustomValidator
{
    public static class MyCustomValidators
    { 
        public static  IRuleBuilderOptions<T,string> HasNoNumber<T>(this IRuleBuilder<T, string> ruleBuilder) 
        {
            return ruleBuilder.Must(g => g
                   .Where(c => char
                   .IsLetter(c) || c == ' ')
                   .Count() == g.Length)
                   .WithMessage("Should not contain any numerical values");
        }

        public static IRuleBuilderOptions<T, string> HasNoSymbol<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(g => g
                   .Where(c => char
                   .IsLetterOrDigit(c) || c == ' ')
                   .Count() == g.Length)
                   .WithMessage("Should not contain any symbol");
        }

        public static IRuleBuilderOptions<T, int> HasNoLetter<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(g => g
                   .ToString()
                   .Where(c => char
                   .IsLetter(c))
                   .Count() == g
                   .ToString().Length)
                   .WithMessage("Should not contain any letter");
        }
    }
}
