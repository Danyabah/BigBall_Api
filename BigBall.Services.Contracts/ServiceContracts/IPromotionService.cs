using BigBall.Services.Contracts.Models;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface IPromotionService
    {
        /// <summary>
        /// Получить список всех <see cref="PromotionModel"/>
        /// </summary>
        Task<IEnumerable<PromotionModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PromotionModel"/> по идентификатору
        /// </summary>
        Task<PromotionModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PromotionModel"/>
        /// </summary>
        Task<PromotionModel> AddAsync(PromotionModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="PromotionModel"/>
        /// </summary>
        Task<PromotionModel> EditAsync(PromotionModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="PromotionModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
