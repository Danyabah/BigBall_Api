using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;

namespace BigBall.Repositories.WriteRepositories
{
    public class InstitutionWriteRepository : BaseWriteRepository<Institution>, IInstitutionWriteRepository, IRepositoryAnchor
    {
        public InstitutionWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {
            
        }
    }
}
