using AutoMapper;
using BigBall.API.Exceptions;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.API.Validators;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBall.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе со скидками и промокодами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Promotion")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService promotionService;
        private readonly IApiValidatorService apiValidator;
        private readonly IMapper mapper;

        public PromotionController(IPromotionService promotionService, IMapper mapper, IApiValidatorService apiValidator)
        {
            this.promotionService = promotionService;
            this.mapper = mapper;
            this.apiValidator = apiValidator;
        }

        /// <summary>
        /// Получить список акций
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PromotionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var promotionModels = await promotionService.GetAllAsync(cancellationToken);
            return Ok(promotionModels.Select(x => mapper.Map<PromotionResponse>(x)));
        }

        /// <summary>
        /// Получить акцию по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PromotionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var promotionModel = await promotionService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PromotionResponse>(promotionModel));
        }

        /// <summary>
        /// Добавить акцию
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PromotionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePromotionRequest model, CancellationToken cancellationToken)
        {
            await apiValidator.ValidateAsync(model, cancellationToken);

            var promotionModel = mapper.Map<PromotionModel>(model);
            var result = await promotionService.AddAsync(promotionModel, cancellationToken);

            return Ok(mapper.Map<PromotionResponse>(result));
        }

        /// <summary>
        /// Изменить акцию
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PromotionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PromotionRequest request, CancellationToken cancellationToken)
        {
            await apiValidator.ValidateAsync(request, cancellationToken);

            var promotionModel = mapper.Map<PromotionModel>(request);
            var result = await promotionService.EditAsync(promotionModel, cancellationToken);

            return Ok(mapper.Map<PromotionResponse>(result));
        }

        /// <summary>
        /// Удалить акцию по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await promotionService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
