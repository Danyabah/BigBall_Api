using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;

namespace BigBall.Repositories.WriteRepositories
{
    public class PaymentWriteRepository : BaseWriteRepository<Payment>, IPaymentWriteRepository, IRepositoryAnchor
    {
        public PaymentWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {

        }
    }
}
