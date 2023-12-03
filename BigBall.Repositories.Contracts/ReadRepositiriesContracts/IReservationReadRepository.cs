using BigBall.Context.Contracts.Models;

namespace BigBall.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Reservation"/>
    /// </summary>
    public interface IReservationReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Reservation"/>
        /// </summary>
        Task<IReadOnlyCollection<Reservation>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Reservation"/> по идентификатору
        /// </summary>
        Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
