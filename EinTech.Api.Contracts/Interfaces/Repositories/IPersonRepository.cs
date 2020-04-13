using System.Collections.Generic;
using System.Threading.Tasks;
using EinTech.Api.Contracts.Entities;

namespace EinTech.Api.Contracts.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonEntity> AddPerson(PersonEntity person);
        Task<IList<PersonEntity>> GetPersons();
        Task<IList<PersonEntity>> GetPersonsByNameOrGroup(string searchCriteria);
    }
}