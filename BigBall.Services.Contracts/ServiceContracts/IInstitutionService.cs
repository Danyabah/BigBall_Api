using BigBall.Services.Contracts.Models;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface IInstitutionService
    {
        /// <summary>
        /// Получить список всех <see cref="InstitutionModel"/>
        /// </summary>
        Task<IEnumerable<InstitutionModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="InstitutionModel"/> по идентификатору
        /// </summary>
        Task<InstitutionModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="InstitutionModel"/>
        /// </summary>
        Task<InstitutionModel> AddAsync(InstitutionModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="InstitutionModel"/>
        /// </summary>
        Task<InstitutionModel> EditAsync(InstitutionModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="InstitutionModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
