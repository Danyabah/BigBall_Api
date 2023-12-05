using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Validators.Institution;
using BigBall.API.Validators.Payment;
using BigBall.API.Validators.Person;
using BigBall.API.Validators.Promotion;
using BigBall.API.Validators.Reservation;
using BigBall.API.Validators.Track;
using BigBall.General;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using BigBall.Services.Contracts.Exeptions;
using FluentValidation;

namespace BigBall.API.Validators
{
    public sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IInstitutionReadRepository institutionRead, IPaymentReadRepository paymentRead,
            IPersonReadRepository personRead, ITrackReadRepository trackRead)
        {
            validators.Add(typeof(CreateInstitutionRequest), new CreateInstitutionRequestValidator());
            validators.Add(typeof(InstitutionRequest), new InstitutionRequestValidator());
            validators.Add(typeof(CreatePaymentRequest), new CreatePaymentRequestValidator());
            validators.Add(typeof(PaymentRequest), new PaymentRequestValidator());
            validators.Add(typeof(CreatePersonRequest), new CreatePersonRequestValidator());
            validators.Add(typeof(PersonRequest), new PersonRequestValidator());
            validators.Add(typeof(CreatePromotionRequest), new CreatePromotionRequestValidator());
            validators.Add(typeof(PromotionRequest), new PromotionRequestValidator());
            validators.Add(typeof(CreateTrackRequest), new CreateTrackRequestValidator());
            validators.Add(typeof(TrackRequest), new TrackRequestValidator());
            validators.Add(typeof(CreateReservationRequest), new CreateReservationRequestValidator(institutionRead,
                paymentRead, personRead, trackRead));
            validators.Add(typeof(ReservationRequest), new ReservationRequestValidator(institutionRead,
               paymentRead, personRead, trackRead));
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new BigBallValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
