using BigBall.Context.Contracts.Models;

namespace BigBall.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Payment"/>
    /// </summary>
    public interface IPaymentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Payment"/>
        /// </summary>
        Task<IReadOnlyCollection<Payment>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Payment"/> по идентификатору
        /// </summary>
        Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Payment"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Payment>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Payment"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
