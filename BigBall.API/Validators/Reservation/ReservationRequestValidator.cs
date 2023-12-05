using BigBall.API.Models.Request;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using FluentValidation;

namespace BigBall.API.Validators.Reservation
{
    /// <summary>
    /// Валидатор для <<see cref="ReservationRequest"/>
    /// </summary>
    public class ReservationRequestValidator : AbstractValidator<ReservationRequest>
    {
        public ReservationRequestValidator(IInstitutionReadRepository institutionRead, IPaymentReadRepository paymentRead,
            IPersonReadRepository personRead, ITrackReadRepository trackRead)
        {
            RuleFor(x => x.PromotionId)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .When(x => x.PromotionId.HasValue);

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await institutionRead.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await paymentRead.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await personRead.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.TrackId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await trackRead.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(x => DateTimeOffset.UtcNow.AddDays(1));

            RuleFor(x => x.Description)
               .MaximumLength(300).WithMessage(MessageForValidation.LengthMessage)
               .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.CountPeople)
                .InclusiveBetween(1, 7).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(100, 20000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
