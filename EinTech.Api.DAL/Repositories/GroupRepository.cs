using System.Collections.Generic;
using System.Threading.Tasks;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EinTech.Api.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApiContext _context;

        public GroupRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IList<GroupEntity>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }
    }
}
