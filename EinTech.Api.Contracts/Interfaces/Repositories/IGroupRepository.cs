using System.Collections.Generic;
using System.Threading.Tasks;
using EinTech.Api.Contracts.Entities;

namespace EinTech.Api.Contracts.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<IList<GroupEntity>> GetGroups();
    }
}