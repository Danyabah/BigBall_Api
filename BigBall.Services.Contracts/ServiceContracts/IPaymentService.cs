using BigBall.Services.Contracts.Models;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface IPaymentService
    {
        /// <summary>
        /// Получить список всех <see cref="PaymentModel"/>
        /// </summary>
        Task<IEnumerable<PaymentModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PaymentModel"/> по идентификатору
        /// </summary>
        Task<PaymentModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PaymentModel"/>
        /// </summary>
        Task<PaymentModel> AddAsync(PaymentModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="PaymentModel"/>
        /// </summary>
        Task<PaymentModel> EditAsync(PaymentModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="PaymentModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
