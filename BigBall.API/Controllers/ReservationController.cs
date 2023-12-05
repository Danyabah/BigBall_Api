using AutoMapper;
using BigBall.API.Exceptions;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ModelsRequest;
using BigBall.Services.Contracts.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBall.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с заказами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IMapper mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            this.reservationService = reservationService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список заказов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var reservationModels = await reservationService.GetAllAsync(cancellationToken);
            return Ok(reservationModels.Select(x => mapper.Map<ReservationResponse>(x)));
        }

        /// <summary>
        /// Получить заказ по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var reservationModel = await reservationService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<ReservationResponse>(reservationModel));
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateReservationRequest model, CancellationToken cancellationToken)
        {
            var reservationModel = mapper.Map<ReservationModelRequest>(model);
            var result = await reservationService.AddAsync(reservationModel, cancellationToken);
            return Ok(mapper.Map<ReservationResponse>(result));
        }

        /// <summary>
        /// Изменить заказ
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(ReservationRequest request, CancellationToken cancellationToken)
        {
            var reservationModel = mapper.Map<ReservationModelRequest>(request);
            var result = await reservationService.EditAsync(reservationModel, cancellationToken);
            return Ok(mapper.Map<ReservationResponse>(result));
        }

        /// <summary>
        /// Удалить заказ по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await reservationService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
