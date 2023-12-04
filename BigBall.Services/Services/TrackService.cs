using AutoMapper;
using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts.Models;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using BigBall.Repositories.Contracts.WriteRepositiriesContracts;
using BigBall.Services.Anchor;
using BigBall.Services.Contracts.Exeptions;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;

namespace BigBall.Services.Services
{
    public class TrackService : ITrackService, IServiceAnchor
    {
        private readonly ITrackReadRepository trackRead;
        private readonly ITrackWriteRepository trackWrite;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TrackService(ITrackReadRepository trackRead, ITrackWriteRepository trackWrite,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.trackRead = trackRead;
            this.trackWrite = trackWrite;
            this.unitOfWork = unitOfWork;
        }

        async Task<TrackModel> ITrackService.AddAsync(TrackModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var track = mapper.Map<Track>(model);

            trackWrite.Add(track);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task ITrackService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var track = await trackRead.GetByIdAsync(id, cancellationToken);

            if (track == null)
            {
                throw new BigBallEntityNotFoundException<Track>(id);
            }

            if(track.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Дорожка с идентификатором {id} уже удалена");
            }

            trackWrite.Delete(track);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TrackModel> ITrackService.EditAsync(TrackModel model, CancellationToken cancellationToken)
        {
            var track = await trackRead.GetByIdAsync(model.Id, cancellationToken);

            if (track == null)
            {
                throw new BigBallEntityNotFoundException<Track>(model.Id);
            }

            track = mapper.Map<Track>(model);

            trackWrite.Update(track);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task<IEnumerable<TrackModel>> ITrackService.GetAllAsync(CancellationToken cancellationToken)
        {
            var tracks = await trackRead.GetAllAsync(cancellationToken);
            
            return tracks.Select(x => mapper.Map<TrackModel>(x));
        }

        async Task<TrackModel?> ITrackService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var track = await trackRead.GetByIdAsync(id, cancellationToken);

            if(track == null)
            {
                throw new BigBallEntityNotFoundException<Track>(id);
            }
            
            return mapper.Map<TrackModel>(track);
        }
    }
}
