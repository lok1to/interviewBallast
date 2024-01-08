using FluentValidation;
using InterviewBallast.Core.Dto.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Core.Validators
{
    public class PlayerValidator : AbstractValidator<PlayerRequest>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.LastName)
               .NotEmpty()
               .NotNull();
        }
    }
}
