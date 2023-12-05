using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class TrackReadRepository : ITrackReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public TrackReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Track>> ITrackReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Track>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Track?> ITrackReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Track>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Track>> ITrackReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => reader.Read<Track>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> ITrackReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Track>().ById(id).AnyAsync(x => !x.DeletedAt.HasValue, cancellationToken);
    }
}
