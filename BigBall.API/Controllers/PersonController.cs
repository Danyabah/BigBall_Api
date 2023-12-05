using AutoMapper;
using BigBall.API.Exceptions;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;
using BigBall.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBall.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с клиентами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var personModels = await personService.GetAllAsync(cancellationToken);
            return Ok(personModels.Select(x => mapper.Map<PersonResponse>(x)));
        }

        /// <summary>
        /// Получить клиента по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var personModel = await personService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(personModel));
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePersonRequest model, CancellationToken cancellationToken)
        {
            var personModel = mapper.Map<PersonModel>(model);
            var result = await personService.AddAsync(personModel, cancellationToken);
            return Ok(mapper.Map<PersonModel>(result));
        }

        /// <summary>
        /// Изменить клиента
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PersonRequest request, CancellationToken cancellationToken)
        {
            var personModel = mapper.Map<PersonModel>(request);
            var result = await personService.EditAsync(personModel, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Удалить клиента по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await personService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
