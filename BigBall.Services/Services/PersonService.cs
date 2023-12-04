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
    public class PersonService : IPersonService, IServiceAnchor
    {
        private readonly IPersonReadRepository personRead;
        private readonly IPersonWriteRepository personWrite;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PersonService(IPersonReadRepository personRead, IPersonWriteRepository personWrite,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.personRead = personRead;
            this.personWrite = personWrite;
            this.mapper = mapper;
        }

        async Task<PersonModel> IPersonService.AddAsync(PersonModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var person = mapper.Map<Person>(model);

            personWrite.Add(person);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task IPersonService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await personRead.GetByIdAsync(id, cancellationToken);

            if(person == null)
            {
                throw new BigBallEntityNotFoundException<Person>(id);
            }

            if(person.DeletedAt.HasValue)
            {
                throw new BigBallInvalidOperationException($"Клиент с идентификатором {id} уже удален");
            }

            personWrite.Delete(person);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PersonModel> IPersonService.EditAsync(PersonModel model, CancellationToken cancellationToken)
        {
            var person = await personRead.GetByIdAsync(model.Id, cancellationToken);

            if (person == null)
            {
                throw new BigBallEntityNotFoundException<Person>(model.Id);
            }

            person = mapper.Map<Person>(model);

            personWrite.Update(person);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model;
        }

        async Task<IEnumerable<PersonModel>> IPersonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var persons = await personRead.GetAllAsync(cancellationToken);

            return persons.Select(x => mapper.Map<PersonModel>(x));
        }

        async Task<PersonModel?> IPersonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await personRead.GetByIdAsync(id, cancellationToken);

            if(person == null)
            {
                throw new BigBallEntityNotFoundException<Person>(id);
            }

            return mapper.Map<PersonModel>(person); 
        }
    }
}
