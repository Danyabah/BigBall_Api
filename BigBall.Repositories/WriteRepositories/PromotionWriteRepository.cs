using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;

namespace BigBall.Repositories.WriteRepositories
{
    public class PromotionWriteRepository : BaseWriteRepository<Promotion>, IPromotionWriteRepository, IRepositoryAnchor
    {
        public PromotionWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {

        }
    }
}
