using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class InstitutionReadRepository : IInstitutionReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public InstitutionReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Institution>> IInstitutionReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Institution>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Address)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Institution?> IInstitutionReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Institution>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Institution>> IInstitutionReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Institution>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Address)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IInstitutionReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Institution>().ById(id).AnyAsync(x => !x.DeletedAt.HasValue, cancellationToken);
    }
}
