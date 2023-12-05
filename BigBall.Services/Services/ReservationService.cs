using AutoMapper;
using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;
using BigBall.Services.Anchor;
using BigBall.Services.Contracts.Exeptions;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ModelsRequest;
using BigBall.Services.Contracts.ServiceContracts;

namespace BigBall.Services.Services
{
    public class ReservationService : IReservationService, IServiceAnchor
    {
        private readonly IReservationReadRepository reservationReadRepository;
        private readonly IReservationWriteRepository reservationWriteRepository;
        private readonly IInstitutionReadRepository institutionReadRepository;
        private readonly IPaymentReadRepository paymentReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IPromotionReadRepository promotionReadRepository;
        private readonly ITrackReadRepository trackReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReservationService(IReservationReadRepository reservationReadRepository, IReservationWriteRepository reservationWriteRepository,
            IInstitutionReadRepository institutionReadRepository,
            IPaymentReadRepository paymentReadRepository, IPersonReadRepository personReadRepository,
            IPromotionReadRepository promotionReadRepository, ITrackReadRepository trackReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.reservationReadRepository = reservationReadRepository;
            this.reservationWriteRepository = reservationWriteRepository;
            this.institutionReadRepository = institutionReadRepository;
            this.paymentReadRepository = paymentReadRepository;
            this.personReadRepository = personReadRepository;
            this.promotionReadRepository = promotionReadRepository;
            this.trackReadRepository = trackReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<ReservationModel> IReservationService.AddAsync(ReservationModelRequest model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var reservation = mapper.Map<Reservation>(model);

            await SetPriceReservation(reservation, cancellationToken);
            await CheckCapacity(reservation.CountPeople, reservation.TrackId, cancellationToken);

            reservationWriteRepository.Add(reservation);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetReservationModelOnMapping(reservation, cancellationToken);
        }

        async Task IReservationService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var reservation = await reservationReadRepository.GetByIdAsync(id, cancellationToken);

            if(reservation == null)
            {
                throw new BigBallEntityNotFoundException<Reservation>(id);
            }    

            if(reservation.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Заказ с идентификатором {id} уже удален");
            }

            reservationWriteRepository.Delete(reservation);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<ReservationModel> IReservationService.EditAsync(ReservationModelRequest model, CancellationToken cancellationToken)
        {
            var reservation = await reservationReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (reservation == null)
            {
                throw new BigBallEntityNotFoundException<Reservation>(model.Id);
            }

            reservation = mapper.Map<Reservation>(model);

            await SetPriceReservation(reservation, cancellationToken);
            await CheckCapacity(reservation.CountPeople, reservation.TrackId, cancellationToken);

            reservationWriteRepository.Update(reservation);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetReservationModelOnMapping(reservation, cancellationToken);
        }

        async Task<IEnumerable<ReservationModel>> IReservationService.GetAllAsync(CancellationToken cancellationToken)
        {
            var reservations = await reservationReadRepository.GetAllAsync(cancellationToken);

            var tracks = await trackReadRepository
                .GetByIdsAsync(reservations.Select(x => x.TrackId).Distinct(), cancellationToken);

            var institutions = await institutionReadRepository
                .GetByIdsAsync(reservations.Select(x => x.InstitutionId).Distinct(), cancellationToken);

            var payments = await paymentReadRepository
                .GetByIdsAsync(reservations.Select(x => x.PaymentId).Distinct(), cancellationToken);

            var persons = await personReadRepository
                .GetByIdsAsync(reservations.Select(x => x.PersonId).Distinct(), cancellationToken);

            var promotions = await promotionReadRepository
                .GetByIdsAsync(reservations.Where(x => x.PromotionId.HasValue)
                .Select(x => x.PromotionId!.Value).Distinct(), cancellationToken);

            var result = new List<ReservationModel>();

            foreach (var reservation in reservations)
            {
                if (!tracks.TryGetValue(reservation.TrackId, out var track) ||
                !institutions.TryGetValue(reservation.InstitutionId, out var institution) ||
                !payments.TryGetValue(reservation.PaymentId, out var payment) ||
                !persons.TryGetValue(reservation.PersonId, out var person))
                {
                    continue;
                }
                else
                {
                    var reservationModel =  mapper.Map<ReservationModel>(reservation);

                    reservationModel.Track = mapper.Map<TrackModel>(track);
                    reservationModel.Institution = mapper.Map<InstitutionModel>(institution);
                    reservationModel.Promotion = reservation.PromotionId.HasValue &&
                                              promotions.TryGetValue(reservation.PromotionId!.Value, out var promotion)
                        ? mapper.Map<PromotionModel>(promotion)
                        : null;
                    reservationModel.Person = mapper.Map<PersonModel>(person);
                    reservationModel.Payment = mapper.Map<PaymentModel>(payment);

                    result.Add(reservationModel);
                }
            }

            return result;
        }

        async Task<ReservationModel?> IReservationService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var reservation = await reservationReadRepository.GetByIdAsync(id, cancellationToken);

            if(reservation == null)
            {
                throw new BigBallEntityNotFoundException<Reservation>(id);
            }

            return await GetReservationModelOnMapping(reservation, cancellationToken);
        }

        async private Task<ReservationModel> GetReservationModelOnMapping(Reservation reservation, CancellationToken cancellationToken)
        {
            var reservationModel = mapper.Map<ReservationModel>(reservation);
            reservationModel.Track = mapper.Map<TrackModel>(await trackReadRepository.GetByIdAsync(reservation.TrackId, cancellationToken));
            reservationModel.Institution = mapper.Map<InstitutionModel>(await institutionReadRepository.GetByIdAsync(reservation.InstitutionId, cancellationToken));
            reservationModel.Payment = mapper.Map<PaymentModel>(await paymentReadRepository.GetByIdAsync(reservation.PaymentId, cancellationToken));
            reservationModel.Promotion = mapper.Map<PromotionModel>(reservation.PromotionId.HasValue
                ? await promotionReadRepository.GetByIdAsync(reservation.PromotionId.Value, cancellationToken)
                : null);
            reservationModel.Track = mapper.Map<TrackModel>(await trackReadRepository.GetByIdAsync(reservation.TrackId, cancellationToken));

            return reservationModel;
        }

        async private Task SetPriceReservation(Reservation reservation, CancellationToken cancellationToken)
        {
            if (reservation.PromotionId.HasValue)
            {
                var promotion = await promotionReadRepository.GetByIdAsync(reservation.PromotionId.Value, cancellationToken);
                reservation.Price -= reservation.Price * (promotion!.PercentageDiscount / 100);
            }
        }

        async private Task CheckCapacity(int count, Guid id, CancellationToken cancellationToken)
        {
            var capacity = await trackReadRepository.GetByIdAsync(id, cancellationToken);

            if ( count > capacity!.Capacity)
            {
                throw new BigBallInvalidOperationException($"Вы не можете забронировать дорожку на {count} человек при вместимости дорожки {capacity.Capacity}");
            }
        }
    }
}
