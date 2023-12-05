using AutoMapper;
using BigBall.API.Exceptions;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBall.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с кинотеатрами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Institution")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService institutionService;
        private readonly IMapper mapper;

        public InstitutionController(IInstitutionService institutionService, IMapper mapper)
        {
            this.institutionService = institutionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список клубов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InstitutionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var institutionModels = await institutionService.GetAllAsync(cancellationToken);
            return Ok(institutionModels.Select(x => mapper.Map<InstitutionResponse>(x)));
        }

        /// <summary>
        /// Получить клуб по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(InstitutionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var institutionModel = await institutionService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<InstitutionResponse>(institutionModel));
        }

        /// <summary>
        /// Добавить клуб
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InstitutionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateInstitutionRequest model, CancellationToken cancellationToken)
        {
            var institutionModel = mapper.Map<InstitutionModel>(model);
            var result = await institutionService.AddAsync(institutionModel, cancellationToken);
            return Ok(mapper.Map<InstitutionModel>(result));
        }

        /// <summary>
        /// Изменить клуб
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(InstitutionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(InstitutionRequest request, CancellationToken cancellationToken)
        {
            var institutionModel = mapper.Map<InstitutionModel>(request);
            var result = await institutionService.EditAsync(institutionModel, cancellationToken);
            return Ok(mapper.Map<InstitutionResponse>(result));
        }

        /// <summary>
        /// Удалить клуб по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await institutionService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
