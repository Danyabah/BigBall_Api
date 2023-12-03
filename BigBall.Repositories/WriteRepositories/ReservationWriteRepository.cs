using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Anchor;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;

namespace BigBall.Repositories.WriteRepositories
{
    public class ReservationWriteRepository : BaseWriteRepository<Reservation>, IReservationWriteRepository, IRepositoryAnchor
    {
        public ReservationWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {

        }
    }
}
