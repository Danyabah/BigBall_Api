using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class ReservationReadRepository : IReservationReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public ReservationReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Reservation>> IReservationReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Reservation>()
                .NotDeletedAt()
                .OrderBy(x => x.Date)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Reservation?> IReservationReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Reservation>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
