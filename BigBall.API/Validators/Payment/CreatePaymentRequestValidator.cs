using BigBall.API.Models.CreateRequest;
using FluentValidation;

namespace BigBall.API.Validators.Payment
{

    /// <summary>
    /// Валидатор для <<see cref="CreatePaymentRequest"/>
    /// </summary>
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
              .NotNull().WithMessage(MessageForValidation.DefaultMessage)
              .Length(5, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .CreditCard().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.Requisites)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(16, 19).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
