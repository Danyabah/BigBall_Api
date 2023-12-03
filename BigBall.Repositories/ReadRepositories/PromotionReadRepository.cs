using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class PromotionReadRepository : IPromotionReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public PromotionReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Promotion>> IPromotionReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Promotion>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Promotion?> IPromotionReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Promotion>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Promotion>> IPromotionReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Promotion>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IPromotionReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Institution>().ById(id).AnyAsync(x => !x.DeletedAt.HasValue, cancellationToken);
    }
}
