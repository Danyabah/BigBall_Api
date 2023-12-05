using BigBall.API.Models.CreateRequest;
using FluentValidation;

namespace BigBall.API.Validators.Track
{
    /// <summary>
    /// Валидатор для <<see cref="CreateTrackRequest"/>
    /// </summary>
    public class CreateTrackRequestValidator : AbstractValidator<CreateTrackRequest>
    {
        public CreateTrackRequestValidator()
        {
            RuleFor(x => x.Number)
                .InclusiveBetween(1, 30).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 7).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Type).IsInEnum().WithMessage(MessageForValidation.DefaultMessage);
        }
    }
}
