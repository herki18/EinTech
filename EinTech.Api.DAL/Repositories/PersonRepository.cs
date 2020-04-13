using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EinTech.Api.DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApiContext _context;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(ApiContext context, ILogger<PersonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PersonEntity> AddPerson(PersonEntity person)
        {
            try
            {
                if (person != null)
                {
                    _context.Persons.Add(person);

                    if (!(await _context.SaveChangesAsync() >= 0))
                    {
                        throw new Exception("Saving Person failed");
                    }

                    return await _context.Persons.FindAsync(person.Id);
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IList<PersonEntity>> GetPersons()
        {
            return await _context.Persons.Include(g => g.Group).ToListAsync();
        }

        public async Task<IList<PersonEntity>> GetPersonsByNameOrGroup(string searchCriteria)
        {
            return await _context.Persons
                .Include(g => g.Group)
                .Where(n => EF.Functions.Like(n.Name, $"%{searchCriteria}%") || EF.Functions.Like(n.Group.Name, $"{searchCriteria}%"))
                .ToListAsync();
        }
    }
}
