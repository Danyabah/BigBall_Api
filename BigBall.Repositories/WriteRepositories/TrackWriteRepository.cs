using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;

namespace BigBall.Repositories.WriteRepositories
{
    public class TrackWriteRepository : BaseWriteRepository<Track>, ITrackWriteRepository, IRepositoryAnchor
    {
        public TrackWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {

        }
    }
}
