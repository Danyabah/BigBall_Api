using BigBall.API.Models.Request;
using FluentValidation;

namespace BigBall.API.Validators.Track
{
    /// <summary>
    /// Валидатор для <<see cref="TrackRequest"/>
    /// </summary>
    public class TrackRequestValidator : AbstractValidator<TrackRequest>
    { 
        public TrackRequestValidator()
        {
            RuleFor(x => x.Number)
                .InclusiveBetween(1, 30).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 7).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Type).IsInEnum().WithMessage(MessageForValidation.DefaultMessage);
        }
    }
}
