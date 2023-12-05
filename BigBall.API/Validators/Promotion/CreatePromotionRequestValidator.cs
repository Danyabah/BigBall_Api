using BigBall.API.Models.CreateRequest;
using FluentValidation;

namespace BigBall.API.Validators.Promotion
{
    /// <summary>
    /// Валидатор для <<see cref="CreatePromotionRequest"/>
    /// </summary>
    public class CreatePromotionRequestValidator : AbstractValidator<CreatePromotionRequest>
    {
        public CreatePromotionRequestValidator()
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
