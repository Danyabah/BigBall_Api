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
    public class PaymentService : IPaymentService, IServiceAnchor
    {
        private readonly IPaymentReadRepository paymentRead;
        private readonly IPaymentWriteRepository paymentWrite;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IPaymentReadRepository paymentRead, IPaymentWriteRepository paymentWrite,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.paymentRead = paymentRead;
            this.paymentWrite = paymentWrite;
            this.mapper = mapper;
        }

        async Task<PaymentModel> IPaymentService.AddAsync(PaymentModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var payment = mapper.Map<Payment>(model);

            paymentWrite.Add(payment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task IPaymentService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var payment = await paymentRead.GetByIdAsync(id, cancellationToken);

            if (payment == null)
            {
                throw new BigBallEntityNotFoundException<Payment>(id);
            }

            if(payment.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Способ оплаты с идентификатором {id} уже удален");
            }

            paymentWrite.Delete(payment);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PaymentModel> IPaymentService.EditAsync(PaymentModel model, CancellationToken cancellationToken)
        {
            var payment = await paymentRead.GetByIdAsync(model.Id, cancellationToken);

            if (payment == null)
            {
                throw new BigBallEntityNotFoundException<Payment>(model.Id);
            }

            payment = mapper.Map<Payment>(model);

            paymentWrite.Update(payment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task<IEnumerable<PaymentModel>> IPaymentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var payments = await paymentRead.GetAllAsync(cancellationToken);
            
            return payments.Select(x => mapper.Map<PaymentModel>(x));
        }

        async Task<PaymentModel?> IPaymentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var payment = await paymentRead.GetByIdAsync(id, cancellationToken);
            
            if(payment == null)
            {
                throw new BigBallEntityNotFoundException<Payment>(id);
            }

            return mapper.Map<PaymentModel>(payment);
        }
    }
}
