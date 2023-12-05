using BigBall.API.Models.CreateRequest;
using FluentValidation;

namespace BigBall.API.Validators.Person
{
    /// <summary>
    /// Валидатор для <<see cref="CreatePersonRequest"/>
    /// </summary>
    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Patronymic)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 99).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .EmailAddress().WithMessage(MessageForValidation.DefaultMessage)
                .MaximumLength(100).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
