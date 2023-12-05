using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class PersonReadRepository : IPersonReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public PersonReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IPersonReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>().ById(id).AnyAsync(x => !x.DeletedAt.HasValue, cancellationToken);
    }
}
