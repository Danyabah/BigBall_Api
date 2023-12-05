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
    /// CRUD контроллер по работе с дорожками
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Track")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService trackService;
        private readonly IMapper mapper;

        public TrackController(ITrackService trackService, IMapper mapper)
        {
            this.trackService = trackService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список дорожек
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TrackResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var trackModels = await trackService.GetAllAsync(cancellationToken);
            return Ok(trackModels.Select(x => mapper.Map<TrackResponse>(x)));
        }

        /// <summary>
        /// Получить дорожку по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var trackModel = await trackService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<TrackResponse>(trackModel));
        }

        /// <summary>
        /// Добавить дорожку
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateTrackRequest model, CancellationToken cancellationToken)
        {
            var trackModel = mapper.Map<TrackModel>(model);
            var result = await trackService.AddAsync(trackModel, cancellationToken);
            return Ok(mapper.Map<TrackResponse>(result));
        }

        /// <summary>
        /// Изменить дорожку
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(TrackRequest request, CancellationToken cancellationToken)
        {
            var trackModel = mapper.Map<TrackModel>(request);
            var result = await trackService.EditAsync(trackModel, cancellationToken);
            return Ok(mapper.Map<TrackResponse>(result));
        }

        /// <summary>
        /// Удалить дорожку по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await trackService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
