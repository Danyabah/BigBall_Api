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
    public class InstitutionService : IInstitutionService, IServiceAnchor
    {
        private readonly IInstitutionReadRepository institutionRead;
        private readonly IInstitutionWriteRepository institutionWrite;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public InstitutionService(IInstitutionReadRepository institutionRead, IInstitutionWriteRepository institutionWrite,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.institutionRead = institutionRead;
            this.institutionWrite = institutionWrite;
            this.unitOfWork = unitOfWork;
        }

        async Task<InstitutionModel> IInstitutionService.AddAsync(InstitutionModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
          
            var institution = mapper.Map<Institution>(model);

            institutionWrite.Add(institution);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task IInstitutionService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var institution = await institutionRead.GetByIdAsync(id, cancellationToken);

            if (institution == null)
            {
                throw new BigBallEntityNotFoundException<Institution>(id);
            }

            if (institution.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Клуб с идентификатором {id} уже удален");
            }

            institutionWrite.Delete(institution);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<InstitutionModel> IInstitutionService.EditAsync(InstitutionModel model, CancellationToken cancellationToken)
        {
            var institution = await institutionRead.GetByIdAsync(model.Id, cancellationToken);

            if (institution == null)
            {
                throw new BigBallEntityNotFoundException<Institution>(model.Id);
            }

            institution = mapper.Map<Institution>(model);

            institutionWrite.Update(institution);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task<IEnumerable<InstitutionModel>> IInstitutionService.GetAllAsync(CancellationToken cancellationToken)
        {
            var institutions = await institutionRead.GetAllAsync(cancellationToken);
            return institutions.Select(x => mapper.Map<InstitutionModel>(x));
        }

        async Task<InstitutionModel?> IInstitutionService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var institution = await institutionRead.GetByIdAsync(id, cancellationToken);

            if(institution == null)
            {
                throw new BigBallEntityNotFoundException<Institution>(id);
            }

            return mapper.Map<InstitutionModel>(institution);
        }
    }
}
