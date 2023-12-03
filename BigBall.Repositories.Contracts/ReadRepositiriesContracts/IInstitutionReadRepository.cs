using BigBall.Context.Contracts.Models;

namespace BigBall.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Institution"/>
    /// </summary>
    public interface IInstitutionReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Institution"/>
        /// </summary>
        Task<IReadOnlyCollection<Institution>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Institution"/> по идентификатору
        /// </summary>
        Task<Institution?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Institution"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Institution>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Institution"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
