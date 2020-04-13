using System.Collections.Generic;

namespace EinTech.Api.Contracts.Entities
{
    public class GroupEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
    }
}