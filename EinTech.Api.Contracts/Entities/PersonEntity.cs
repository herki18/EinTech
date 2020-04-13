namespace EinTech.Api.Contracts.Entities
{
    public class PersonEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupEntity Group { get; set; }
        public int GroupId { get; set; }
    }
}