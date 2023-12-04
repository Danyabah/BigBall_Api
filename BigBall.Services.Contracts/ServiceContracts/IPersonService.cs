using BigBall.Services.Contracts.Models;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface IPersonService
    {
        /// <summary>
        /// Получить список всех <see cref="PersonModel"/>
        /// </summary>
        Task<IEnumerable<PersonModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PersonModel"/> по идентификатору
        /// </summary>
        Task<PersonModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PersonModel"/>
        /// </summary>
        Task<PersonModel> AddAsync(PersonModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="PersonModel"/>
        /// </summary>
        Task<PersonModel> EditAsync(PersonModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="PersonModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
