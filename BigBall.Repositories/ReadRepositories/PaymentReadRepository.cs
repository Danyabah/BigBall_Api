using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity.Repositories;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Repositories.ReadRepositories
{
    public class PaymentReadRepository : IPaymentReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public PaymentReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Payment>> IPaymentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Payment>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Payment?> IPaymentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Payment>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Payment>> IPaymentReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Payment>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IPaymentReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Institution>().ById(id).AnyAsync(x => !x.DeletedAt.HasValue, cancellationToken);
    }
}
