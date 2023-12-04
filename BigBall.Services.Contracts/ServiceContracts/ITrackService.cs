using BigBall.Services.Contracts.Models;

namespace BigBall.Services.Contracts.ServiceContracts
{
    public interface ITrackService
    {
        /// <summary>
        /// Получить список всех <see cref="TrackModel"/>
        /// </summary>
        Task<IEnumerable<TrackModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TrackModel"/> по идентификатору
        /// </summary>
        Task<TrackModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="TrackModel"/>
        /// </summary>
        Task<TrackModel> AddAsync(TrackModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="TrackModel"/>
        /// </summary>
        Task<TrackModel> EditAsync(TrackModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="TrackModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
