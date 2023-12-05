using BigBall.API.Models.CreateRequest;
using FluentValidation;

namespace BigBall.API.Validators.Institution
{
    /// <summary>
    /// Валидатор для <<see cref="CreateInstitutionRequest"/>
    /// </summary>
    public class CreateInstitutionRequestValidator : AbstractValidator<CreateInstitutionRequest>
    {
        public CreateInstitutionRequestValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(3, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(10, 100).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Email)
               .EmailAddress().WithMessage(MessageForValidation.DefaultMessage)
               .MaximumLength(100).WithMessage(MessageForValidation.LengthMessage)
               .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.OpeningTime)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(5).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.ClosingTime)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(5).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
