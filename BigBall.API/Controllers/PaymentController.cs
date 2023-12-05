using AutoMapper;
using BigBall.API.Exceptions;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.API.Validators;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;
using BigBall.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBall.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе со способами оплаты
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IApiValidatorService apiValidator;
        private readonly IMapper mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper, IApiValidatorService apiValidator)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
            this.apiValidator = apiValidator;
        }

        /// <summary>
        /// Получить список способов оплаты
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var paymentModels = await paymentService.GetAllAsync(cancellationToken);
            return Ok(paymentModels.Select(x => mapper.Map<PaymentResponse>(x)));
        }

        /// <summary>
        /// Получить способ оплаты по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var paymentModel = await paymentService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PaymentResponse>(paymentModel));
        }

        /// <summary>
        /// Добавить способ оплаты
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePaymentRequest model, CancellationToken cancellationToken)
        {
            await apiValidator.ValidateAsync(model, cancellationToken);

            var paymentModel = mapper.Map<PaymentModel>(model);
            var result = await paymentService.AddAsync(paymentModel, cancellationToken);

            return Ok(mapper.Map<PaymentModel>(result));
        }

        /// <summary>
        /// Изменить способ оплаты
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PaymentRequest request, CancellationToken cancellationToken)
        {
            await apiValidator.ValidateAsync(request, cancellationToken);

            var paymentModel = mapper.Map<PaymentModel>(request);
            var result = await paymentService.EditAsync(paymentModel, cancellationToken);

            return Ok(mapper.Map<PaymentResponse>(result));
        }

        /// <summary>
        /// Удалить способ оплаты по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await paymentService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
