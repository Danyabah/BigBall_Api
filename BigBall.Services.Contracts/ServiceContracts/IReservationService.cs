using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ModelsRequest;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface IReservationService
    {
        /// <summary>
        /// Получить список всех <see cref="ReservationModel"/>
        /// </summary>
        Task<IEnumerable<ReservationModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ReservationModel"/> по идентификатору
        /// </summary>
        Task<ReservationModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ReservationModel"/>
        /// </summary>
        Task<ReservationModel> AddAsync(ReservationModelRequest model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="ReservationModel"/>
        /// </summary>
        Task<ReservationModel> EditAsync(ReservationModelRequest source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="ReservationModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
