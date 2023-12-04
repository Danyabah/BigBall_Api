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
    public class PromotionService : IPromotionService, IServiceAnchor
    {
        private readonly IPromotionReadRepository promotionRead;
        private readonly IPromotionWriteRepository promotionWrite;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PromotionService(IPromotionReadRepository promotionRead, IPromotionWriteRepository promotionWrite,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.promotionRead = promotionRead;
            this.promotionWrite = promotionWrite;
            this.mapper = mapper;
        }

        async Task<PromotionModel> IPromotionService.AddAsync(PromotionModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var promotion = mapper.Map<Promotion>(model);

            promotionWrite.Add(promotion);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task IPromotionService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var promotion = await promotionRead.GetByIdAsync(id, cancellationToken);

            if(promotion == null)
            {
                throw new BigBallEntityNotFoundException<Promotion>(id);
            }

            if(promotion.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Акция (промокод) с идентификатором {id} уже удалена");
            }

            promotionWrite.Delete(promotion);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PromotionModel> IPromotionService.EditAsync(PromotionModel model, CancellationToken cancellationToken)
        {
            var promotion = await promotionRead.GetByIdAsync(model.Id, cancellationToken);

            if (promotion == null)
            {
                throw new BigBallEntityNotFoundException<Promotion>(model.Id);
            }

            promotion = mapper.Map<Promotion>(model);

            promotionWrite.Update(promotion);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task<IEnumerable<PromotionModel>> IPromotionService.GetAllAsync(CancellationToken cancellationToken)
        {
            var promotions = await promotionRead.GetAllAsync(cancellationToken);

            return promotions.Select(x => mapper.Map<PromotionModel>(x));
        }

        async Task<PromotionModel?> IPromotionService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var promotion = await promotionRead.GetByIdAsync(id, cancellationToken); 

            if(promotion == null)
            {
                throw new BigBallEntityNotFoundException<Promotion>(id);
            }

            return mapper.Map<PromotionModel>(promotion);
        }
    }
}
