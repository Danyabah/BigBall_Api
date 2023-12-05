using BigBall.API.Models.Request;
using FluentValidation;

namespace BigBall.API.Validators.Promotion
{
    /// <summary>
    /// Валидатор для <<see cref="PromotionRequest"/>
    /// </summary>
    public class PromotionRequestValidator : AbstractValidator<PromotionRequest>
    {
        public PromotionRequestValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
            .NotNull().WithMessage(MessageForValidation.DefaultMessage)
            .Length(5, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.PercentageDiscount)
                .InclusiveBetween(1, 30).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
