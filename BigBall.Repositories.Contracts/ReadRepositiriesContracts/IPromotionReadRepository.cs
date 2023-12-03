using BigBall.Context.Contracts.Models;

namespace BigBall.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Promotion"/>
    /// </summary>
    public interface IPromotionReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Promotion"/>
        /// </summary>
        Task<IReadOnlyCollection<Promotion>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Promotion"/> по идентификатору
        /// </summary>
        Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Promotion"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Promotion>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Promotion"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
